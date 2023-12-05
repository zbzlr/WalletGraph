using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Net;
using System.Net.Mail;

namespace WalletGraphs.Models
{
    public class EmailSenderService : IEmailSender
    {
        public Task SendEmail(string message, string subject, string recieverMail)
        {
            var email = "mail-adresi";
            var password = "sifre";

            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(email, password)
            };

            return client.SendMailAsync(
                new MailMessage(from:email,
                                to: recieverMail,
                                subject,
                                message)
                );
        }    
    }
} 

    
