using EventBooking.Application.Interfaces;
using EventBooking.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EventBooking.Application.Commands.EventCommands
{
    public record CreateEventCommand : IRequest<Response<string>>, IEventBase
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public DateTime EventDate { get; init; }
        public string Venue { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int AvailableTickets { get; init; }
        public IFormFile? Image { get; init; }
    }
}
