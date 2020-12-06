using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using ChessWeb.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ChessWeb.Service.Services
{
    public class SmtpOptions
    {
        public const string SectionName = "SmtpOptions";
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class MailSenderService : IMailSender
    {
        public IConfiguration Configuration { get; set; }
        public SmtpOptions _smtpOptions { get; set; }

        public MailSenderService(IConfiguration configuration, IOptions<SmtpOptions> smtpOptions)
        {
            Configuration = configuration;
            _smtpOptions = smtpOptions.Value;
        }

        public async Task SendMailAsync(string recipientName, string subject, string message)
        {
            var fromMessage = new MailAddress(_smtpOptions.Username, "ChessWeb");
            var mailAddress = recipientName;
            var toMessage = new MailAddress(mailAddress);
            var mailMessage = new MailMessage(fromMessage, toMessage)
            {
                Subject = subject,
                // todo: uncomment when make html template
                IsBodyHtml = true,
            };

            // todo: add html string.Format(messageTemplate, message)
            var htmlMessage = message;

            // 5. set message's body
            mailMessage.Body = $"<p>{htmlMessage}</p>";

            // 6. initialize smtpClient
            using var smtpClient = new SmtpClient(_smtpOptions.Host, _smtpOptions.Port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_smtpOptions.Username, _smtpOptions.Password),
            };

            smtpClient.Send(mailMessage);
        }
    }
}