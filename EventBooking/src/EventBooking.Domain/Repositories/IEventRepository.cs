using EventBooking.Domain.Entities;

namespace EventBooking.Domain.Repositories
{

    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<IEnumerable<Event>> GetEventsByUserIdAsync(string userId);
        Task<bool> HasAvailableSeatsAsync(int eventId, int requestedSeats);
    }
}
