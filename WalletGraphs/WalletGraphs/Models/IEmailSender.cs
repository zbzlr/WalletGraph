namespace WalletGraphs.Models
{
    public interface IEmailSender
    {
        Task SendEmail(string message, string subject, string recieverMail );
    }
}