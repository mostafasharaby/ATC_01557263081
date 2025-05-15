using AutoMapper;
using EventBooking.Application.Commands.EventCommands;
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

            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<UpdateEventCommand, Event>()
                .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.AvailableTickets))
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.EventTags, opt => opt.Ignore());

            CreateMap<CreateEventCommand, Event>()
               .ForMember(dest => dest.AvailableSeats, opt => opt.MapFrom(src => src.AvailableTickets))
               .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
               .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
               .ForMember(dest => dest.Bookings, opt => opt.Ignore())
               .ForMember(dest => dest.EventTags, opt => opt.Ignore());


        }
    }
}
