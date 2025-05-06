using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Commands.AuthCommands
{
    public record GoogleLoginCallbackCommand()
        : IRequest<AuthResponse>;
}