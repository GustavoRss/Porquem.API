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
    public class NewEntityMappingProfile : Profile
    {
        public NewEntityMappingProfile()
        {
            CreateMap<NewPhilanthropicEntity, PhilanthropicEntity>()
                .ForMember(d => d.CreatedAt, o => o.MapFrom(x=> DateTime.Now))
                .ForMember(d => d.DtOpening, o => o.MapFrom(x=>x.DtOpening.Date));
            CreateMap<Address, NewAddress>().ReverseMap();
            CreateMap<Campaign, NewCampaign>().ReverseMap();
            CreateMap<Campaign, ViewEntityCampaigns>().ReverseMap();
            CreateMap<Document, NewDocument>().ReverseMap();
            CreateMap<PhilanthropicEntity, ChangeStatus>().ReverseMap();
            CreateMap<PhilanthropicEntity, ViewPhilanthropicEntity>().ReverseMap();
            CreateMap<HelpItem, ViewHelpItem>().ReverseMap();
        }
    }
}
