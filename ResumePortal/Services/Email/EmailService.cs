using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace ResumePortal.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string receiver, string subject, string message)
        {
            var email = _configuration["EmailSettings:Email"];
            var password = _configuration["EmailSettings:Password"];
            var host = _configuration["EmailSettings:Host"];
            var port = _configuration["EmailSettings:Port"];

            var emailMessage = new MimeMessage(); 
            emailMessage.From.Add(new MailboxAddress("Contact Email", email));
            emailMessage.To.Add(new MailboxAddress("Receiver Email", receiver));
            emailMessage.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = message;
            emailMessage.Body = bodyBuilder.ToMessageBody();

            //var client = new SmtpClient(host, int.Parse(port))
            //{
            //    EnableSsl = true,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(email, password)
            //};

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(host, int.Parse(port), true);
                await client.AuthenticateAsync(email, password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            //return client.SendMailAsync(
            //    new MailMessage(from: email,
            //                    to: receiver,
            //                    subject,
            //                    message
            //                    ));
        }
    }
}
