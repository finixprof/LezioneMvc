using System.ComponentModel.DataAnnotations;

namespace Site.Models.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Username required!")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password required!")]
        public string Password { get; set; }
    }
}