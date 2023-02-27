using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Helpers;
using Site.Helpers.Extensions;
using Site.Models;
using Site.Models.Entities;
using Site.Models.Views;
using System.Data;

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

        [HttpGet]
        public IActionResult CreaPersonale()
        {
            var model = new CreaPersonaleViewModel
            {
                Pagina = Costanti.Pagine.Personale,
                ListaPersonale = DatabaseHelper.GetAllPersonale()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreaPersonale(PersonaleDto dto)
        {
            var model = new CreaPersonaleViewModel
            {
                Pagina = Costanti.Pagine.Personale,
                ListaPersonale = DatabaseHelper.GetAllPersonale(),
                Cognome = dto.Cognome
            };
            if (ModelState.IsValid)
            {
                if (dto.SuperioreId == 0)
                    dto.SuperioreId = null;
                //insert su database
                var utente = DatabaseHelper.SalvaPersonale(dto);

                //dopo aver creato la modifica, manderemo la.
                ViewData["MsgOk"] = "Record aggiunto con successo";
                return View(model);
            }

            ViewData["MsgKo"] = "Record aggiunto con successo";
            return View(model);
        }

    }
}
