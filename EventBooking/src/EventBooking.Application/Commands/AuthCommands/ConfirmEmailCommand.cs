using EventBooking.Application.Responses;
using EventBooking.Domain.Entities;
using MediatR;

namespace EventBooking.Application.Commands.AuthCommands
{
    public record ConfirmEmailCommand(string UserId, string Token)
        : IRequest<Response<AppUser>>;
}