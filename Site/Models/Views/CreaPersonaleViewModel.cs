using Site.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Site.Models.Views
{
    public class CreaPersonaleViewModel : BaseViewModel
    {
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
        public int SuperioreId { get; set; }

        public List<Personale> ListaPersonale { get; set; }
    }
}
