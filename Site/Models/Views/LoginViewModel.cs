using System.ComponentModel.DataAnnotations;

namespace Site.Models.Views
{
    public class LoginViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Username required!")]
        [Display(Name = "Username/Email")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password required!")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
