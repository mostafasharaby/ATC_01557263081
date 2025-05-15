using EventBooking.Application.DTOs;
using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Queries.EventQueries
{
    public record GetEventByIdQuery(int Id) : IRequest<Response<EventDto>> { }

}
