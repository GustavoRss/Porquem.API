using Microsoft.Extensions.DependencyInjection;
using PQ.Manager.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PQ.WebApi.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(NewEntityMappingProfile),
                typeof(UpdateEntityMappingProfile),
                typeof(UpdateCampaignMappingProfile),
                typeof(UserMappingProfile),
                typeof(NewCampaignMappingProfile),
                typeof(NewHelpItemMappingProfile),
                typeof(VisitorMappingProfile));
        }
    }
}
