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
        public async Task ConfirmEmailAsync(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Invalid token or email, could not confirm email");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new Exception("Invalid token or email, could not confirm email");
            }
        }

        public async Task ForgotPasswordAsync(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("No user with this email");
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
                throw new Exception("Could not reset password, invalid token or email");
            }
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception("Could not reset password, invalid token or email");
            }
        }

        public async Task SignInAsync(LoginViewModel loginViewModel)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user == null)
            {
                throw new Exception("Invalid Credentials, Signin failed");
            }
            var canSignIn = await _userManager.IsEmailConfirmedAsync(user);
            if (!canSignIn)
            {
                throw new Exception("Please Confirm Email before attempting to sign in");
            }
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
            var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if(user != null)
            {
                throw new Exception("Email Already exists");
            }
            AppUser appUser = new AppUser() { 
                Name = registerViewModel.Name,
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };

            var result = await _userManager.CreateAsync(appUser, registerViewModel.Password!);
            
            if (result.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                var encodedEmail = UrlEncoder.Default.Encode(appUser.Email);
                var encodedToken = UrlEncoder.Default.Encode(token);
                var resetLink = $"https://localhost:7199/Account/ConfirmEmail?email={encodedEmail}&token={encodedToken}";
                var body = $"<p>Click the following link to confirm your email: <a href='{resetLink}'>Confirm Email</a></p>";

                await _emailService.SendEmailAsync(appUser.Email, "Email Confirmation Link", body);
            }
            else
            {
                throw new Exception("Could not create user");
            }
        }
    }
}
