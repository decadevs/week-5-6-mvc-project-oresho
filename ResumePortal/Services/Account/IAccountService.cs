﻿using ResumePortal.Models.ViewModels;

namespace ResumePortal.Services.Account
{
    public interface IAccountService
    {
        Task SignInAsync(LoginViewModel loginViewModel);
        Task SignOutAsync();
        Task SignUpAsync(RegisterViewModel registerViewModel);
        Task ConfirmPassword();
        Task ForgotPasswordAsync(string email);
        Task ResetPasswordAsync(string email, string newPassword, string token);
    }
}
