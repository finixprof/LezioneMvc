using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Helpers.Extensions;
using Site.Models.Entities;

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
    }
}
