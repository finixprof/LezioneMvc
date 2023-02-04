using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Site.Helpers;
using Site.Models;
using Site.Models.Entities;

namespace Site.Controllers
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
            var model = new BaseViewModel();
            model.Pagina= Costanti.Pagine.Home;
            model.Testo = "Questa applicazione serve per gestire le visite di un Ospedale.";
            return View(model);
        }

        public IActionResult Privacy()
        {
            var model = new BaseViewModel();
            model.Pagina = Costanti.Pagine.Privacy;
            model.Testo = "Use this page to detail your site's privacy policy.";
            return View(model);
        }

        public IActionResult Personale()
        {
            //Recupero dei dati da database
            var listaPersonale = DatabaseHelper.GetAllPersonale();
            //creazione del modello da passare alla view
            var model = new PersonaleViewModel();
            model.Pagina = Costanti.Pagine.Personale;
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
            model.Pagina = Costanti.Pagine.Personale;

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
            model.Pagina = Costanti.Pagine.Pazienti;

            model.ListaPazienti = listaPazienti;
            model.Testo = "Lista del pazienti.";
            return View(model);
        }

        public IActionResult DettaglioPaziente(int id)
        {
            //Recupero dei dati da database con filtro id
            var paziente = DatabaseHelper.GetPazienteById(id);
            var model = new DettaglioPazienteViewModel();
            model.Pagina = Costanti.Pagine.Pazienti;

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

        public IActionResult Visite()
        {
            //Recupero dei dati da database
            var listaVisite = DatabaseHelper.GetAllVisite();
            var model = new VisiteViewModel();
            model.Pagina = Costanti.Pagine.Visite;

            if (listaVisite == null || listaVisite.Count == 0)
            {
                model.Testo = "Non ci sono visite";
                model.ListaVisite = new List<Visita>();
                return View(model);
            }
            //creazione del modello da passare alla view
            model.ListaVisite = listaVisite;
            model.Testo = "Lista del visite.";
            return View(model);
        }

        public IActionResult DettaglioVisita(int id)
        {
            //Recupero dei dati da database con filtro id
            var visita = DatabaseHelper.GetVisitaById(id);
            var model = new DettaglioVisitaViewModel();
            model.Pagina = Costanti.Pagine.Visite;

            if (visita == null)
            {
                model.Testo = "Errore, visita non trovata";
                return View(model);
            }
            //creazione del modello da passare alla view
            model.Item = visita;
            model.Testo = "Dati del visita.";
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}