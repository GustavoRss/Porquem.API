using AutoMapper;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.Campaign;
using PQ.CoreShared.ModelViews.HelpItem;
using PQ.CoreShared.ModelViews.PhilanthropicEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Manager.Mappings
{
    public class NewCampaignMappingProfile : Profile
    {
        public NewCampaignMappingProfile()
        {
            CreateMap<NewCampaign, Campaign>()
                .ForMember(d => d.StartDate, o => o.MapFrom(x => x.StartDate.Date))
                .ForMember(d => d.EndDate, o => o.MapFrom(x => x.EndDate.Date));
            CreateMap<PhilanthropicEntity, NewPhilanthropicEntity>().ReverseMap();
            CreateMap<PhilanthropicEntity, PhilanthropicEntityCampaign>().ReverseMap();
            CreateMap<HelpItem, ViewHelpItem>().ReverseMap();
        }
    }
}
