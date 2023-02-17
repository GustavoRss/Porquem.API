using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PQ.WebApi.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PorQuemAPI",
                    Version = "v1",
                    Description = "API desenvolvida para o projeto PorQuem",
                    TermsOfService = new Uri("https://porquem.netlify.app/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Gustavo Reis",
                        Email = "gustavoreisdev@gmail.com",
                        Url = new Uri("https://porquem.netlify.app/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Termo de Licença de uso",
                        Url = new Uri("https://porquem.netlify.app/")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insira o token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference= new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id ="Bearer"
                        }
                    },
                        new string[]{ }
                    }
                });

                c.AddFluentValidationRules();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);
                c.IncludeXmlComments(xmlPath);
                xmlPath = Path.Combine(AppContext.BaseDirectory, "PQ.CoreShared.xml");
                c.IncludeXmlComments(xmlPath);

            });
        }
        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "PorQuemAPI");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
