using System.ComponentModel.DataAnnotations;


namespace TasksControllerApp.Entities.RegisterViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(2, ErrorMessage = "Username must be at least 2 characters long.")]
        public string Username  { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password should be at least 8 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "Password must include at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
