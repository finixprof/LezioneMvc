using Site.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Site.Models.Views
{
    public class ModificaPersonaleViewModel : BaseViewModel
    {

        public ModificaPersonaleViewModel() { }


        public ModificaPersonaleViewModel(Personale p)
        {
            Id = p.Id;
            Cognome = p.Cognome;
            DataNascita = p.DataNascita;
            Professione = p.Professione;
            Reparto = p.Reparto;
            Stipendio = p.Stipendio;
            SuperioreId = p.SuperioreId;
        }

        public ModificaPersonaleViewModel(PersonaleDto p)
        {
            //controllo che id sia valorizzato
            if (p != null && p.Id != null && p.Id > 0)
            {
                Id = p.Id.Value;
            }
            Cognome = p.Cognome;
            DataNascita = p.DataNascita;
            Professione = p.Professione;
            Reparto = p.Reparto;
            Stipendio = p.Stipendio;
            SuperioreId = p.SuperioreId;
        }


        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [Display(Name = "Data Nascita")]
        [DataType(DataType.Date)]
        public DateTime DataNascita { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Professione { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Reparto { get; set; }


        [Required(ErrorMessage = "Campo obbligatorio")]
        public double Stipendio { get; set; }

        [Display(Name = "Superiore")]
        public int? SuperioreId { get; set; }

        public List<Personale> ListaPersonale { get; set; }
    }
}
