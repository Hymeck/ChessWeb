using System.Threading.Tasks;
using ChessWeb.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

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
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("У Артемки в хатке", _smtpOptions.Username));
            emailMessage.To.Add(new MailboxAddress("", recipientName));
            emailMessage.Subject = subject;

            var htmlMessage = string.Format("<p>{0}</p>", message);
            
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port, false);
                await client.AuthenticateAsync(_smtpOptions.Username, _smtpOptions.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}