using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TasksControllerApp.Entities.RegisterViewModel;
using TasksControllerApp.Models;
using TeamProjectMVC.Entity.Enums;

namespace TasksControllerApp.Services
{
    public class AccountService
    {
        private readonly UserManager<User> _userManager;

        public AccountService(UserManager<User> userManager) => _userManager = userManager;

        public string HandleModelStateErrors(ModelStateDictionary modelState, string defaultErrorMessage = "Validation failed.")
        {
            if (modelState.IsValid) return "success";

            var errorMessages = modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return errorMessages.Count > 0 ? errorMessages[0] : defaultErrorMessage;
        }

        public async Task<(bool IsAuthenticated, User user)> CheckUserAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                var roles = await _userManager.GetRolesAsync(user);
                return (true, user)!;
            }

            return (false, null)!;
        }

        public async Task<bool> RegisterUser(RegisterViewModel model)
        {
            var newUser = new User()
            {
                Email = model.Email,
                UserName = model.Username
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            await _userManager.AddToRoleAsync(newUser, ERole.USER.ToString());
            return true;
        }

        public async Task<bool> AddUser(RegisterViewModel model, string role)
        {
            var newUser = new User()
            {
                Email = model.Email,
                UserName = model.Username
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, model.Password);
            await _userManager.AddToRoleAsync(newUser, role);
            return true;
        }

        public async Task<bool> CheckUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                return true;
            }
            return false;
        }
    }
}
