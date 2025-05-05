using EventBooking.Domain.Entities;

namespace EventBooking.Domain.Repositories
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<bool> HasAvailableSeatsAsync(int eventId, int requestedSeats);
    }
}
