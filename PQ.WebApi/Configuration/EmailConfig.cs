using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PQ.CoreShared.ModelViews.Email;
using PQ.Data.Services;
using PQ.Manager.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQ.WebApi.Configuration
{
    
        public static class EmailConfig
        {
            public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
            {
            services.Configure<EmailSettings>(configuration.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
        
    }
}
