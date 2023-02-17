using AutoMapper;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews.Campaign;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Manager.Mappings
{
    public class UpdateCampaignMappingProfile : Profile
    {
        public UpdateCampaignMappingProfile()
        {
            CreateMap<UpdateCampaign, Campaign>()
                .ForMember(d => d.UpdatedAt, o => o.MapFrom(x => DateTime.Now))
                .ForMember(d => d.StartDate, o => o.MapFrom(x => x.StartDate.Date))
                .ForMember(d => d.EndDate, o => o.MapFrom(x => x.EndDate.Date));
        }
    }
}
