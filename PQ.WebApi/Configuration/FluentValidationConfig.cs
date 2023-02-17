using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PQ.Manager.Validator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PQ.WebApi.Configuration
{
    public static class FluentValidationConfig
    {
        public static void AddFluentValidationConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(x=>x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .AddFluentValidation(p => {
                p.RegisterValidatorsFromAssemblyContaining<NewEntityValidator>();
                p.RegisterValidatorsFromAssemblyContaining<ChangeEntityValidator>();
                p.RegisterValidatorsFromAssemblyContaining<NewAddressValidator>();
                p.RegisterValidatorsFromAssemblyContaining<NewDocumentValidator>();
                p.RegisterValidatorsFromAssemblyContaining<NewUserValidator>();
                p.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
            });
        }
    }
}
