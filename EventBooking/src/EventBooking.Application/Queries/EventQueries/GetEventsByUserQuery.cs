using EventBooking.Application.DTOs;
using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Queries.EventQueries
{
    public record GetEventsByUserQuery : IRequest<Response<List<EventDetailsDto>>>
    {
        public string? UserId { get; init; }
        public bool OnlyUpcoming { get; init; } = true;
    }
}
