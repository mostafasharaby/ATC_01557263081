using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Commands.EventCommands
{
    public record DeleteEventCommand(int Id) : IRequest<Response<string>>;
}
