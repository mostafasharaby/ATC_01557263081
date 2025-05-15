using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Commands.AuthCommands
{
    public record ForgotPasswordCommand(string? Email)
        : IRequest<Response<string>>;
}