using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Commands.AuthCommands
{
    public record ResetPasswordCommand(string? Email, string? Token, string? NewPassword, string? ConfirmNewPassword)
        : IRequest<Response<string>>;
}