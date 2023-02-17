using PQ.Core.Domain;
using AutoMapper;
using PQ.CoreShared.ModelViews;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Manager.Mappings
{
    public class UpdateEntityMappingProfile : Profile
    {
        public UpdateEntityMappingProfile()
        {
            CreateMap<UpdatePhilanthropicEntity, PhilanthropicEntity>()
                .ForMember(d => d.UpdatedAt, o => o.MapFrom(x => DateTime.Now))
                .ForMember(d => d.DtOpening, o => o.MapFrom(x => x.DtOpening.Date));
        }
    }
}
