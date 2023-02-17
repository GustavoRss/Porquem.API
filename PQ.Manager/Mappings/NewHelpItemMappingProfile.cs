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
    public class NewHelpItemMappingProfile : Profile
    {
        public NewHelpItemMappingProfile()
        {
            CreateMap<NewHelpItem, HelpItem>();
            CreateMap<ViewHelpItem, HelpItem>();
        }
    }
}
