using Microsoft.AspNetCore.Mvc;
using ResumePortal.Models.ViewModels;
using ResumePortal.Services.Account;

namespace ResumePortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            await _accountService.SignUpAsync(model);
            return RedirectToAction("SignupMessage", "Account");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            await _accountService.SignInAsync(model);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            await _accountService.ForgotPasswordAsync(email);
            return View();
        }

        public IActionResult ResetPassword(string email, string token)
        {
            ResetPasswordViewModel viewModel = new ResetPasswordViewModel();
            viewModel.Email = email;
            viewModel.Token = token;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string token, ResetPasswordViewModel model)
        {
            await _accountService.ResetPasswordAsync(email, model.Password, token);
            return RedirectToAction("Login");
        }

        public IActionResult SignupMessage()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            await _accountService.ConfirmEmailAsync(email, token);
            return View();
        }
    }
}
