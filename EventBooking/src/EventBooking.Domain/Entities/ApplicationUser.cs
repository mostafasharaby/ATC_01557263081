using Microsoft.AspNetCore.Identity;

namespace EventBooking.Domain.Entities
{

    public class AppUser : IdentityUser
    {
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
