using Microsoft.AspNetCore.Identity;

namespace EventBooking.Domain.Entities
{

    public class AppUser : IdentityUser
    {

        public string? ImageUrl { get; set; }
        public string? RefreshToken { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenExpiryTime { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
