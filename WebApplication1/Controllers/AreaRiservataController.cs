using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AreaRiservataController : Controller
    {
        public IActionResult Index()
        {
            var utenteLoggato = HttpContext.Session.GetString("UtenteLoggato");
            if (utenteLoggato== null)
                return RedirectToAction("Login", "Account");
            ViewData["UtenteLoggato"] = utenteLoggato;

            return View();
        }
    }
}
