using Microsoft.Extensions.DependencyInjection;
using PQ.Data.Repository;
using PQ.Manager.Implementation;
using PQ.Manager.Interfaces;
using PQ.Manager.Interfaces.Managers;
using PQ.Manager.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQ.WebApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfig(this IServiceCollection services)
        {
            services.AddScoped<IPhilanthropicEntityRepository, PhilanthropicEntityRepository>();
            services.AddScoped<IPhilanthropicEntityManager, PhilanthropicEntityManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IDataAdmRepository, DataAdmRepository>();
            services.AddScoped<IDataAdmManager, DataAdmManager>();
            services.AddScoped<ICampaignManager, CampaignManager>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            services.AddScoped<IVisitorManager, VisitorManager>();
            services.AddScoped<IVisitorRepository, VisitorRepository>();
            services.AddScoped<IHelpItemManager, HelpItemManager>();
            services.AddScoped<IHelpItemRepository, HelpItemRepository>();
        }
    }
}
