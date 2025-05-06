using EventBooking.Application.DTOs;
using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Queries.EventQueries
{
    public record GetEventListQuery : IRequest<Response<List<EventDto>>>
    {
        public string? SearchTerm { get; init; }
        public string? Category { get; init; }
        public DateTime? FromDate { get; init; }
        public DateTime? ToDate { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
