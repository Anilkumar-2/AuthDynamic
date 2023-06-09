namespace DataAccessLayer.Repositories
{
    using AutoMapper;
    using DataAccessLayer.DTO;
    using DataAccessLayer.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>()
              .ForMember(dest => dest.RoleName, act => act.MapFrom(src => src.Role.RoleName))
                .ReverseMap();
        }

    }
}
