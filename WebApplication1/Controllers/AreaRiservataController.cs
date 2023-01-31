using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helpers.Extensions;
using WebApplication1.Models.Entities;

namespace WebApplication1.Controllers
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
