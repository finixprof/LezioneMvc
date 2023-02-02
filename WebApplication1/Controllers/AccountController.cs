﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Helpers.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            model.Pagina = -1;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var model = new LoginViewModel();
            model.Pagina = -1;
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
    }
}
