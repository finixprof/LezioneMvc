using System.ComponentModel.DataAnnotations;

namespace Site.Models.Entities
{
    public class PersonaleDto
    {
        [Required(ErrorMessage = "Campo obbligatorio")]

        public string Cognome { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Professione { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [DataType(DataType.Date)]
        public DateTime DataNascita { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Reparto { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        public double Stipendio { get; set; }

        public int? SuperioreId { get; set; }
    }
}
