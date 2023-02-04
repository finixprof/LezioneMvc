using Microsoft.AspNetCore.Mvc;
using Site.Helpers;
using Site.Models.Views;
using Site.Helpers.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Site.Models.Dtos;
using Site.Models;

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
                var password = CryptoHelper.HashSHA256($"{utente.Id}*{dto.Password}+{utente.DataCreazione.Value.ToShortDateString()}-{utente.DataCreazione.Value.ToShortTimeString()}");
                utente = DatabaseHelper.Login(dto.Username, password);
                if (utente != null)
                {
                    //ok devo loggarmi -> uso la session al momento, poi passerò all'identity di .NET

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, utente.Email),
                        ///new Claim("Finix", {Your Value})
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));

                    //mettiamo l'utente in sessione
                    //HttpContext.Session.SetString("UtenteLoggato", utente.Email);
                    HttpContext.Session.SetObject("UtenteLoggato", utente);
                    //redirect ad area riservata
                    return RedirectToAction("Index", "AreaRiservata");
                }

                // devo mostrare un errore in pagina
                ViewData["MsgKo"] = "Non esiste un utente con Username e Password indicate";
                return View(model);
            }
            //restituire un errore

            //per recupoerare gli errori indiuviduati dal ModelState
            var errori = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

            var errore = "Compilare correttamente Username e Password";
            ViewData["MsgKo"] = errore;
            return View(model);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            //svuotiamo la sessione, potremmo anche 
            //HttpContext.Session.SetString("UtenteLoggato", null);            
            HttpContext.Session.Clear();
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
                var exists = DatabaseHelper.ExistUtenteByUsernameOrEmail(dto.Username, dto.Email);
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
                var utente = DatabaseHelper.InsertUtente(dto);

                //3)Cifro la password e aggiorno il database (UPDATE)

                //4)Invio mail di conferma

                //5)Messaggio di registrazione completata e di andare a confermare la mail
                ViewData["MsgOk"] = "Registrazione completata, confermare l'indirizzo email";
                return View(model);
            }
            //mando un errore alla pagina
            var errore = "Compilare correttamente tutti i campi";
            ViewData["MsgKo"] = errore;
            return View(model);

        }
    }
}
