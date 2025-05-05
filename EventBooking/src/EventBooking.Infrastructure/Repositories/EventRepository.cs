using EventBooking.Domain.Entities;
using EventBooking.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventBooking.Infrastructure.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Event> GetByIdWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(e => e.EventTags)
                    .ThenInclude(et => et.Tag)
                .Include(e => e.Bookings)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<List<Event>> GetAllWithDetailsAsync()
        {
            return await _dbSet
                .Include(e => e.EventTags)
                    .ThenInclude(et => et.Tag)
                .Include(e => e.Bookings)
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Event>> FindWithDetailsAsync(Expression<Func<Event, bool>> predicate)
        {
            return await _dbSet
                .Include(e => e.EventTags)
                    .ThenInclude(et => et.Tag)
                .Include(e => e.Bookings)
                .Where(e => !e.IsDeleted)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<bool> HasAvailableSeatsAsync(int eventId, int requestedSeats)
        {
            var eventEntity = await _dbSet
                .Include(e => e.Bookings)
                .FirstOrDefaultAsync(e => e.Id == eventId && !e.IsDeleted);

            if (eventEntity == null)
                return false;

            var bookedSeats = eventEntity.Bookings.Sum(b => b.TicketCount);
            var availableSeats = eventEntity.AvailableSeats - bookedSeats;

            return availableSeats >= requestedSeats;
        }
    }
}
