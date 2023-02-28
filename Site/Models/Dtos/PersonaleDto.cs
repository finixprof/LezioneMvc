using System.ComponentModel.DataAnnotations;

namespace Site.Models.Entities
{
    public class PersonaleDto
    {
        public int? Id { get; set; }

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

        public Personale GetPersonale()
        {
            return new Personale()
            {
                Id = Id != null ? Id.Value : 0,
                Cognome = Cognome,
                DataNascita = DataNascita,
                Professione = Professione,
                Reparto = Reparto,
                Stipendio = Stipendio,
                SuperioreId = SuperioreId // != null ? SuperioreId.Value : 0,
            };
        }
    }
}
