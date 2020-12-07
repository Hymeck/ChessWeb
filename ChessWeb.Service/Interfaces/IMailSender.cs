using System.Threading.Tasks;

namespace ChessWeb.Service.Interfaces
{
    public interface IMailSender
    {
        Task SendMailAsync(string recipientName, string subject, string message);
    }
}