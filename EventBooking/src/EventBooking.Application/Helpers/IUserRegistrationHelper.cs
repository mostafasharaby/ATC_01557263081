using EventBooking.Application.Responses;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Helpers
{
    public interface IUserRegistrationHelper
    {
        Task<Response<string>> RegisterUserAsync(AppUser user, string password, string role);
    }
}
