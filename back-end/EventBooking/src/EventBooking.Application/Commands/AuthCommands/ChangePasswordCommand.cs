using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Commands.AuthCommands
{
    public record ChangePasswordCommand(string? CurrentPassword, string? NewPassword, string? ConfirmNewPassword)
        : IRequest<Response<string>>;
}