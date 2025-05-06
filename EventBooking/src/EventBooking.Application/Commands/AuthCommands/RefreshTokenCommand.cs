using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Commands.AuthCommands
{
    public record RefreshTokenCommand(string? AccessToken, string? RefreshToken)
        : IRequest<Response<AuthResponse>>;
}