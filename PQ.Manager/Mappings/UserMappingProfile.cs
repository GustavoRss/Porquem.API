using AutoMapper;
using PQ.Core.Domain;
using PQ.CoreShared.ModelViews;
using PQ.CoreShared.ModelViews.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace PQ.Manager.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserView>().ReverseMap();
            CreateMap<User, NewUser>().ReverseMap();
            CreateMap<User, LoggedUser>().ReverseMap();
            CreateMap<Role, RoleView>().ReverseMap();
            CreateMap<Role, ReferenceRole>().ReverseMap();
        }
    }
}
