using PQ.CoreShared.ModelViews.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PQ.Manager.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailView emailRequest);
        Task Send(string to, string subject, string html, string from = null);
    }
}
