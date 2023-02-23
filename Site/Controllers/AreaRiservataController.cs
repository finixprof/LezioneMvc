using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Helpers;
using Site.Helpers.Extensions;
using Site.Models;
using Site.Models.Entities;
using Site.Models.Views;

namespace Site.Controllers
{
    [Authorize]
    public class AreaRiservataController : Controller
    {
        public IActionResult Index()
        {
            var utenteLoggato = HttpContext.Session.GetObject<Utente>("UtenteLoggato");
            if (utenteLoggato== null)
                return RedirectToAction("Login", "Account");
            ViewData["UtenteLoggato"] = utenteLoggato;

            return View();
        }

        public IActionResult CreaPersonale()
        {
            var model = new CreaPersonaleViewModel
            {
                Pagina = Costanti.Pagine.Personale,
                ListaPersonale = DatabaseHelper.GetAllPersonale()
            };
            return View(model);
        }
    }
}
