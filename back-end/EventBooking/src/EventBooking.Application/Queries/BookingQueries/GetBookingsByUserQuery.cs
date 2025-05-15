using EventBooking.Application.DTOs;
using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Queries.BookingQueries
{
    public record GetBookingsByUserQuery(string UserId) : IRequest<Response<List<BookingDto>>>;


}
