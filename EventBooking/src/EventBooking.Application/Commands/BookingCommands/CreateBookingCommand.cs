using EventBooking.Application.Interfaces;
using EventBooking.Application.Responses;
using MediatR;
namespace EventBooking.Application.Commands.BookingCommands
{
    public class CreateBookingCommand : IBookingBase, IRequest<Response<string>>
    {
        public int EventId { get; set; }
        public int TicketCount { get; set; }
    }
}
