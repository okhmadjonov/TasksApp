using System.ComponentModel.DataAnnotations;

namespace TasksControllerApp.Entities.LoginViewModel
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Email is required.")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Email or password is incorrect.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Email or password is incorrect.")]
        public string Password { get; set; }
    }
}
