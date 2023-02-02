using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RegistrazioneViewModel : BaseViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required] //dobbiamo anche definire le regole della password
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage ="La password di conferma deve coincidere con la password")]
        [DataType(DataType.Password)]
        public string ConfermaPassword { get; set; }
    }
}
