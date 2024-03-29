namespace ResumePortal.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string receiver, string subject, string message);
    }
}
