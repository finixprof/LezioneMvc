using System.ComponentModel.DataAnnotations;

namespace Site.Models.Views
{
    public class RegistrazioneViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio")]
        //dobbiamo anche definire le regole della password
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "La password di conferma deve coincidere con la password")]
        [DataType(DataType.Password)]
        public string ConfermaPassword { get; set; }
    }
}
