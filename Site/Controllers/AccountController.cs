using Microsoft.AspNetCore.Mvc;
using Site.Helpers;
using Site.Models.Views;
using Site.Helpers.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Site.Models.Dtos;
using Site.Models;
using Site.Models.Entities;

namespace Site.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            model.Pagina = Costanti.Pagine.Login;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var model = new LoginViewModel();
            model.Pagina = Costanti.Pagine.Login;
            if (ModelState.IsValid) //verifica che il model sia valido, seguendo le indicazioni delle dataannotation
            {
                var utente = DatabaseHelper.GetUtenteByUsername(dto.Username);
                if (utente != null)
                {
                    var password = CryptoHelper.HashSHA256($"{utente.Id}*{dto.Password}+{utente.DataCreazione.Value.ToShortDateString()}-{utente.DataCreazione.Value.ToShortTimeString()}");
                    utente = DatabaseHelper.Login(dto.Username, password);
                    if (utente != null)
                    {
                        //ok devo loggarmi -> uso la session al momento, poi passerò all'identity di .NET
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, utente.Email),
                            ///new Claim("Finix", {Your Value}) //possiamo aggiungere qualsiasi chiave personalizzata e assegnare un valore
                        };

                        var claimsIdentity = new ClaimsIdentity(
                            claims,
                            CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity));

                        //mettiamo l'utente in sessione
                        HttpContext.Session.SetObject("UtenteLoggato", utente);
                        //redirect ad area riservata
                        return RedirectToAction("Index", "AreaRiservata");
                    }
                }

                // devo mostrare un errore in pagina
                ViewData["MsgKo"] = "Non esiste un utente con Username e Password indicate";
                return View(model);
            }
            //restituire un errore            
            //var errori = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage); //per recupoerare gli errori indiuviduati dal ModelState
            var errore = "Compilare correttamente Username e Password";
            ViewData["MsgKo"] = errore;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("UtenteLoggato");
            await HttpContext.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Registrazione()
        {
            var model = new RegistrazioneViewModel();
            model.Pagina = Costanti.Pagine.Registrazione;
            return View(model);
        }

        [HttpPost]
        public IActionResult Registrazione(RegistrazioneDto dto)
        {
            var model = new RegistrazioneViewModel();
            model.Pagina = Costanti.Pagine.Registrazione;
            if (ModelState.IsValid)
            {
                //proseguo con la registrazione

                //1) controllare che username o email non siano stati già utilizzati (SELECT)
                // in caso di esistenza mostrare l'errore
                var utente = DatabaseHelper.GetUtenteByEmail(dto.Email);
                if (utente == null || utente.Username != dto.Username)
                {
                    var exists = DatabaseHelper.ExistUtenteByUsername(dto.Username);
                    if (exists == null)
                    {
                        ViewData["MsgKo"] = Costanti.Errori.ServizioNonDisponibile;
                        return View(model);
                    }
                    else if (exists.Value)
                    {
                        ViewData["MsgKo"] = "Username o email già utilizzati";
                        return View(model);
                    }

                    //2)Inserisco i dati su database (INSERT)
                    if (utente == null)
                        utente = DatabaseHelper.InsertUtente(dto);
                }
                else if (utente.DataUltimaModifica != null)
                {
                    // esiste un utente con la mail indicata ed ha completato la registrazione
                    ViewData["MsgKo"] = "Username o email già utilizzati";
                    return View(model);
                }
                else if (utente.Username != dto.Username)
                {
                    var exists = DatabaseHelper.ExistUtenteByUsername(dto.Username);
                    if (exists == null)
                    {
                        ViewData["MsgKo"] = Costanti.Errori.ServizioNonDisponibile;
                        return View(model);
                    }
                    else if (exists.Value)
                    {
                        ViewData["MsgKo"] = "Username o email già utilizzati";
                        return View(model);
                    }
                }


                //3)Cifro la password e aggiorno il database (UPDATE)
                var daCifrare = $"{utente.Id}*{dto.Password}+{utente.DataCreazione.Value.ToShortDateString()}-{utente.DataCreazione.Value.ToShortTimeString()}";
                utente.Password = CryptoHelper.HashSHA256(daCifrare);
                DatabaseHelper.UpdateUtente(utente);

                //4)Invio mail di conferma
                //creo il token e il link da mettere nella mail
                var tokenDaCifrare = $"{utente.Id}*{dto.Email}+{utente.DataCreazione.Value.ToShortDateString()}-{utente.DataCreazione.Value.ToShortTimeString()}";
                var token = CryptoHelper.Encrypt(tokenDaCifrare);
                var link = PathHelper.GetUrlToConfirmEmail(HttpContext.Request, utente.Id, token);
                //invio mail


                //5)Messaggio di registrazione completata e di andare a confermare la mail
                ViewData["MsgOk"] = "Registrazione completata, confermare l'indirizzo email";
                return View(model);
            }
            //mando un errore alla pagina
            var errore = "Compilare correttamente tutti i campi";
            ViewData["MsgKo"] = errore;
            return View(model);

        }

        [HttpGet]
        public IActionResult ConfermaRegistrazione(int id, string token)
        {
            //dal token nel link estraggo l'indirizzo email e data di creazione
            //decifrare il token
            var tokenDecifrato = CryptoHelper.Decrypt(token);

            //var email = 
            //var dataCreazione = 

                //update dataultimamodifica in utente con where ad hoc
                
            return View();
        }
    }
}
