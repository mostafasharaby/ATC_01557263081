using EventBooking.Application.Responses;
using Microsoft.AspNetCore.Authentication;

namespace EventBooking.Application.Interfaces
{
    public interface IGoogleService
    {
        AuthenticationProperties GetGoogleLoginProperties(string redirectUri);
        Task<AuthResponse> GoogleLoginCallbackAsync();
    }
}
