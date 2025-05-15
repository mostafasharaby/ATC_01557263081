using EventBooking.Application.Interfaces;
using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Commands.BookingCommands
{
    public class UpdateBookingCommand : IBookingBase, IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int TicketCount { get; set; }
    }

}
