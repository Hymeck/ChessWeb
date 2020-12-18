using System;
using System.Threading.Tasks;
using ChessWeb.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace ChessWeb.Service.Services
{
    public class SmtpOptions
    {
        public string Username { get; }
        public string Password { get; }
        public string Host { get; }
        public int Port { get; }

        public SmtpOptions(string username, string password, string host, int port)
        {
            Username = username;
            Password = password;
            Host = host;
            Port = port;
        }

        public static SmtpOptions FromConfiguration(IConfiguration configuration)
        {
            string userName;
            string password;
            string host;
            int port;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                // require secrets.json
                userName = configuration["SmtpUsername"];
                password = configuration["SmtpPassword"];
                host = configuration["SmtpHost"];
                port = int.Parse(configuration["SmtpPort"]);
            }

            else
            {
                userName = Environment.GetEnvironmentVariable("SmtpUsername");
                password = Environment.GetEnvironmentVariable("SmtpPassword");
                host = Environment.GetEnvironmentVariable("SmtpHost");
                port = int.Parse(Environment.GetEnvironmentVariable("SmtpPort"));
            }
            
            return new SmtpOptions(userName, password, host, port);
        }
    }

    public class MailSenderService : IMailSender
    {
        public SmtpOptions _smtpOptions { get; set; }

        public MailSenderService(IConfiguration configuration)
        {
            _smtpOptions = SmtpOptions.FromConfiguration(configuration);
        }

        public async Task SendMailAsync(string recipientName, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(nameof(ChessWeb), _smtpOptions.Username));
            emailMessage.To.Add(new MailboxAddress("", recipientName));
            emailMessage.Subject = subject;

            var htmlMessage = string.Format("<p>{0}</p>", message);
            
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_smtpOptions.Host, _smtpOptions.Port, false);
            await client.AuthenticateAsync(_smtpOptions.Username, _smtpOptions.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}