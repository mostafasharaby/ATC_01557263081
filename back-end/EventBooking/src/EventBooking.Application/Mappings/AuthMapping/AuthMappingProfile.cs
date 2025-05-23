﻿using Auth.Application.Commands;
using AutoMapper;
using EventBooking.Application.Commands.AuthCommands;
using EventBooking.Application.DTOs;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Mappings
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegisterAdminCommand, AppUser>();
            CreateMap<LoginCommand, AppUser>();
            CreateMap<AppUser, UserDto>()
             .ConstructUsing(user => new UserDto(
                 user.Id,
                 user.UserName,
                 user.Email,
                 user.PhoneNumber,
                 user.ImageUrl
             ));
            CreateMap<AppUser, UserDto>().ReverseMap();

            CreateMap<UpdateAppUserCommand, AppUser>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateProfileCommand, AppUser>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}
