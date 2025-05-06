using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Commands.BookingCommands
{
    public record CancelBookingCommand(int Id) : IRequest<Response<string>>;

}
