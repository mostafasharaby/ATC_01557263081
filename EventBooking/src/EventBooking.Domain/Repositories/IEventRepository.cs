using EventBooking.Domain.Entities;
using System.Linq.Expressions;

namespace EventBooking.Domain.Repositories
{

    public interface IEventRepository : IGenericRepository<Event>
    {
        Task<Event> GetByIdWithDetailsAsync(int id);
        Task<List<Event>> GetAllWithDetailsAsync();
        Task<List<Event>> FindWithDetailsAsync(Expression<Func<Event, bool>> predicate);
        Task<bool> HasAvailableSeatsAsync(int eventId, int requestedSeats);
    }
}
