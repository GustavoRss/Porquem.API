
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PQ.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQ.WebApi.Configuration
{
    public static class DataBaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<PQContext>(options => options.UseMySql(Environment.GetEnvironmentVariable("PQ_MYSQL"), new MySqlServerVersion(new Version(8, 0, 11)), options => options.EnableRetryOnFailure()));
        }
        
        public static void UseDatabaseConfiguration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<PQContext>();
            //context.Database.Migrate();
            //context.Database.EnsureCreated();
        }
    }
}
