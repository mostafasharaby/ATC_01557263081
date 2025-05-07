using AutoMapper;
using EventBooking.Application.Commands.BookingCommands;
using EventBooking.Application.DTOs;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Mappings.BookingMapping
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<CreateBookingCommand, Booking>();
            CreateMap<UpdateBookingCommand, Booking>();

            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.Name))
                .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Event.EventDate))
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Event.Venue));

            CreateMap<Booking, BookingDto>()
               .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.Name))
               .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Event.EventDate))
               .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Event.Venue)).ReverseMap();

        }
    }
}
