namespace ResumePortal.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string message);
    }
}
