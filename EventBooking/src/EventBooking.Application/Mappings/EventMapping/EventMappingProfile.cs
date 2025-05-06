using AutoMapper;
using EventBooking.Application.DTOs;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Mappings.EventMapping
{
    internal class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, EventDto>();
            CreateMap<Event, EventDto>().ReverseMap();


        }
    }
}
