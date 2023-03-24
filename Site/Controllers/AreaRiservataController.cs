using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Site.Helpers;
using Site.Helpers.Extensions;
using Site.Models;
using Site.Models.Dtos;
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
            if (utenteLoggato == null)
                return RedirectToAction("Login", "Account");
            ViewData["UtenteLoggato"] = utenteLoggato;
            var model = new AreaRiservataViewModel();
            model.Id = utenteLoggato.Id;
            model.FileFoto = utenteLoggato.Foto;
            model.Utente = utenteLoggato;
            return View(model);
        }

        [HttpPost]
        public IActionResult ModificaFoto(UtenteDto utenteDto)
        {
            var utenteLoggato = HttpContext.Session.GetObject<Utente>("UtenteLoggato");
            if (utenteLoggato == null)
                return RedirectToAction("Login", "Account");
            ViewData["UtenteLoggato"] = utenteLoggato;
            //se arriva un'immagine salvarla su filesystem (wwwroot/uploads/utente/{id}/<file>)
            //e modificare il db impostando il campo foto a <file>

            if (utenteDto.FileFoto != null)
            {
                //1) aggiorno database
                utenteLoggato.Foto = utenteDto.FileFoto.FileName;
                DatabaseHelper.SalvaUtente(utenteLoggato);
                HttpContext.Session.SetObject("UtenteLoggato", utenteLoggato);

                //2) creo il file su filesystem
                var path = PathHelper.GetPathUtente(utenteDto.Id); //recupero il percorso dell'utente
                if (!Directory.Exists(path))
                {
                    //1)creazione cartella nazioni/id in uploads se non esiste con id quello del modello
                    Directory.CreateDirectory(path);
                }
                //salvare il contenuto di FileFoto nel percorso creato
                var filePath = path + "\\" + utenteLoggato.Foto;
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        utenteDto.FileFoto.CopyTo(fileStream);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            var model = new AreaRiservataViewModel();
            model.Id = utenteLoggato.Id;
            model.FileFoto = utenteLoggato.Foto;
            model.Utente = utenteLoggato;
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult ScaricaExcelPersonale()
        {
            var lista = DatabaseHelper.GetAllPersonale();

            byte[] fileBytes = ExcelHelper.CreaFileExcelListaPersonale(lista);

            //byte[] fileBytes = System.IO.File.ReadAllBytes(@"e:\personale.xlsx");
            string fileName = "personale.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

            //return View(); //deve essere un file non una view
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

            ViewData["MsgKo"] = "Errore, riprova più tardi";
            return View(model);
        }


        [HttpGet]
        public IActionResult ModificaPersonale(int id)
        {
            var personale = DatabaseHelper.GetPersonaleById(id);
            var model = new ModificaPersonaleViewModel(personale)
            {
                Pagina = Costanti.Pagine.Personale,
                ListaPersonale = DatabaseHelper.GetAllPersonale()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ModificaPersonale(PersonaleDto dto)
        {
            var model = new ModificaPersonaleViewModel(dto)
            {
                Pagina = Costanti.Pagine.Personale,
                ListaPersonale = DatabaseHelper.GetAllPersonale(),
            };
            if (ModelState.IsValid && dto.Id != null && dto.Id.Value > 0)
            {
                //insert su database
                var utente = DatabaseHelper.SalvaPersonale(dto);

                //dopo aver creato la modifica, manderemo la.
                ViewData["MsgOk"] = "Record aggiornato con successo";
                return View(model);
            }

            ViewData["MsgKo"] = "Errore, riprova più tardi";
            return View(model);
        }

        [HttpGet]
        public IActionResult CancellaPersonale(int id)
        {
            DatabaseHelper.DeletePersonaleById(id);

            return RedirectToAction("Personale", "Home");
        }

    }
}
