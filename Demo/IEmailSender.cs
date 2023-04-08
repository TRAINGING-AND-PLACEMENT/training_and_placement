using Demo.Models;

namespace Demo
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
