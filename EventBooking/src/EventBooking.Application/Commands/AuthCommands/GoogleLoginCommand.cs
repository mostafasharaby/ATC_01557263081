using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace EventBooking.Application.Commands.AuthCommands
{
    public record GoogleLoginCommand(string? RedirectUri)
        : IRequest<AuthenticationProperties>;
}