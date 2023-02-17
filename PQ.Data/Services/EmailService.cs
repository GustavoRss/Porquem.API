using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Utils;
using PQ.CoreShared.ModelViews.Email;
using PQ.Manager.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Data.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings; 

        public EmailService(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }
        public async Task SendEmailAsync(EmailView emailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.Mail);
            email.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
            email.Subject = emailRequest.Subject;
            var builder = new BodyBuilder();

            var signature = string.Format(@"
            <p>--<br>
            <img src=""https://docs.google.com/uc?&id=1dgYTfTG_3A3InaqbZXarjEA3PBoP24lp&revid=0B2u7MNBp7TuNRklTWWQxYy9pUll4eWQwSXVSRjdLNVNIZEZ3PQ"">");

            builder.HtmlBody = emailRequest.Body + signature;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.Mail, Environment.GetEnvironmentVariable("PQ_PASSWORD_OUTL"));
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task Send(string to, string subject, string html, string from = null)
        {
            EmailView email = new EmailView
            {
                Body = html,
                ToEmail = to,
                Subject = subject
            };
            await SendEmailAsync(email);
        }

    }
}
