using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using ResumePortal.Models.Entities;
using ResumePortal.Models.ViewModels;
using ResumePortal.Services.Email;
using System.Text.Encodings.Web;
using System.Web;

namespace ResumePortal.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        
        public AccountService(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailService = emailService;
        }
        public Task ConfirmPassword()
        {
            throw new NotImplementedException();
        }

        public async Task ForgotPasswordAsync(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedEmail = UrlEncoder.Default.Encode(email);
            var encodedToken = UrlEncoder.Default.Encode(token);
            var resetLink = $"https://localhost:7199/Account/ResetPassword?email={encodedEmail}&token={encodedToken}";
            Console.WriteLine(token);
            var body = $"<p>Click the following link to reset your password: <a href='{resetLink}'>Reset Password</a></p>";

            await _emailService.SendEmailAsync(email, "Reset Password Link", body);

        }

        public async Task ResetPasswordAsync(string email, string newPassword, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Could not reset password, invalid token");
            }
        }

        public async Task SignInAsync(LoginViewModel loginViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, true, false);
            if (!result.Succeeded)
            {
                throw new Exception("Invalid Credentials, Signin failed");
            }
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task SignUpAsync(RegisterViewModel registerViewModel)
        {
            AppUser appUser = new AppUser() { 
                Name = registerViewModel.Name,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };

            var result = await _userManager.CreateAsync(appUser, registerViewModel.Password!);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, true);
            }
            else
            {
                throw new Exception("Could not create user");
            }
        }
    }
}
