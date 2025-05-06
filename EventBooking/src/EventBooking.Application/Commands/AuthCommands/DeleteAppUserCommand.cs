using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Commands.AuthCommands
{
    public record DeleteAppUserCommand(string? Id)
        : IRequest<Response<string>>;
}