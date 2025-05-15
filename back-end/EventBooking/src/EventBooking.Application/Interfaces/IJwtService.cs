using EventBooking.Application.Responses;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Interfaces
{
    public interface IJwtService
    {
        Task<AuthResponse> GenerateJwtToken(AppUser user);
        Task<AuthResponse> RefreshToken(string expiredToken, string refreshToken);
    }
}
