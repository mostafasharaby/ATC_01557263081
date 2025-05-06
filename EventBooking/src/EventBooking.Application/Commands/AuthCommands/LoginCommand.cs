using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Commands.AuthCommands
{
    public record LoginCommand(string? Email, string? Password)
        : IRequest<Response<AuthResponse>>;
}