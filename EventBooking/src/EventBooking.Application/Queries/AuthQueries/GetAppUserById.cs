using EventBooking.Application.DTOs;
using EventBooking.Application.Responses;
using MediatR;

namespace Auth.Application.Queries
{
    public record GetAppUserById(string UserId) : IRequest<Response<UserDto>>;
}
