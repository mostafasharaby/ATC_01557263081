using EventBooking.Application.DTOs;
using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Queries.BookingQueries
{
    public record GetBookingByIdQuery(int Id) : IRequest<Response<BookingDto>>;

}
