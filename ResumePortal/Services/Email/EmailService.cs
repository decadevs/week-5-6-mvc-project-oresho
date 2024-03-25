using System.Net.Mail;
using System.Net;

namespace ResumePortal.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task SendEmailAsync(string subject, string message)
        {
            var email = _configuration["EmailSettings:Email"];
            var password = _configuration["EmailSettings:Password"];
            var host = _configuration["EmailSettings:Host"];
            var port = _configuration["EmailSettings:Port"];
            var receiver = _configuration["EmailSettings:Receiver"];
            var client = new SmtpClient(host, int.Parse(port))
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, password)
            };

            return client.SendMailAsync(
                new MailMessage(from: email,
                                to: receiver,
                                subject,
                                message
                                ));
        }
    }
}
