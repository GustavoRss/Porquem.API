using AutoMapper;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.HelpItem;
using PQ.CoreShared.ModelViews.Visitor;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Manager.Mappings
{
    public class VisitorMappingProfile : Profile
    {
        public VisitorMappingProfile()
        {
            CreateMap<PhilanthropicEntity, ViewVisitorEntities>().ReverseMap();
            CreateMap<PhilanthropicEntity, ViewProfileEntity>().ReverseMap();
            CreateMap<Campaign, ViewVisitorCampaigns>().ReverseMap(); 
            CreateMap<Campaign, ViewProfileCampaign>().ReverseMap();
            CreateMap<HelpItem, ViewHelpItem>().ReverseMap();
        }
    }
}
