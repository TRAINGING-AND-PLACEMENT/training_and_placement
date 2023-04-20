using System.Net.Mail;
using MailKit.Net.Smtp;
using System.Net;
using Demo.Models;
using MailKit;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;

namespace Demo
{
    public class EmailSender : IEmailSender
    {
        private readonly MailSettings _mailSettings;
        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        /*public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("trainnplacement.scet@gmail.com", "1234scet")
            };

            return client.SendMailAsync(
                new MailMessage(from: "trainnplacement.scet@gmail.com",
                                to: email,
                                subject,
                                message
                                ));
        }*/
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            MailboxAddress emailFrom = new MailboxAddress(_mailSettings.Name, _mailSettings.EmailId);
            email.From.Add(emailFrom);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.TextBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.EmailId, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

        }
    }
}
