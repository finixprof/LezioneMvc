using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Personale()
        {
            //Recupero dei dati da database
            var listaPersonale = DatabaseHelper.GetAllPersonale();
            //creazione del modello da passare alla view
            var model = new PersonaleViewModel();
            model.ListaPersonale = listaPersonale;
            model.Testo = "Lista del personale.";
            return View(model);
        }

        public IActionResult DettaglioPersonale(int id)
        {
            //Recupero dei dati da database con filtro id
            var personale = DatabaseHelper.GetPersonaleById(id);
            //creazione del modello da passare alla view
            var model = new DettaglioPersonaleViewModel();
            model.Item = personale;
            model.Testo = "Dati del dipendente personale.";
            return View(model);
        }


        public IActionResult Pazienti()
        {
            //Recupero dei dati da database
            var listaPazienti = DatabaseHelper.GetAllPazienti();
            //creazione del modello da passare alla view
            var model = new PazientiViewModel();
            model.ListaPazienti = listaPazienti;
            model.Testo = "Lista del pazienti.";
            return View(model);
        }

        public IActionResult DettaglioPaziente(int id)
        {
            //Recupero dei dati da database con filtro id
            var paziente = DatabaseHelper.GetPazienteById(id);
            var model = new DettaglioPazienteViewModel();
            if (paziente == null)
            {
                model.Testo = "Errore, paziente non trovato";
                return View(model);
            }
            //creazione del modello da passare alla view
            model.Item = paziente;
            model.Testo = "Dati del paziente.";
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}