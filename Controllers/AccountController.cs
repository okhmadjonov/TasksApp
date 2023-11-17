using Microsoft.AspNetCore.Mvc;
using TasksControllerApp.Models.LoginViewModel;
using TasksControllerApp.Models.RegisterViewModel;
using TasksControllerApp.Services;

namespace TasksControllerApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;


        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            TempData.Clear();
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var validationError = _accountService.HandleModelStateErrors(ModelState, "Please enter valid data.");
            if (validationError != "success")
            {
                TempData["Error"] = validationError;
                TempData.Keep("Error");
                return View();
            }

            var (isAuthenticated, userRole, userId, username) = await _accountService.CheckUserAsync(loginViewModel.Email, loginViewModel.Password);

            if (!isAuthenticated)
            {
                TempData["Error"] = "Email or password is incorrect";
                return View();
            }

            return RedirectToAction("Index", "Home"); //, new { role = userRole, id = userId, userName = username });
        }

        [HttpGet]
        public IActionResult Register()
        {
            TempData.Clear();
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var validationError = _accountService.HandleModelStateErrors(ModelState, "Please enter valid data.");
            if (validationError != "success")
            {
                TempData["Error"] = validationError;
                return View();
            }

            var isAuthenticated = await _accountService.CheckUser(model.Email);

            if (isAuthenticated)
            {
                TempData["Error"] = "User with this email already exists";
                return View();
            }

            await _accountService.RegisterUser(model);

            ViewBag.Success = "Registration successful! You will be redirected to the login page in 3 seconds.";

            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}
