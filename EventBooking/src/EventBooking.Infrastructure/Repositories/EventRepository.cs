using EventBooking.Domain.Entities;
using EventBooking.Domain.Repositories;
using EventBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventBooking.Infrastructure.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {
        }

        public async override Task<Event> GetByIdAsync(int id)
        {
            return await _dbContext.Events
                .Include(e => e.EventTags)
                    .ThenInclude(et => et.Tag)
                .Include(e => e.Bookings)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async override Task<List<Event>> GetAllAsync()
        {
            return await _dbContext.Events
                 .Include(e => e.EventTags)
                     .ThenInclude(et => et.Tag)
                 .Include(e => e.Bookings)
                 .ToListAsync();
        }
        public async override Task<List<Event>> GetByAsync(Expression<Func<Event, bool>> expression)
        {
            return await _dbContext.Events
                 .Include(e => e.EventTags)
                     .ThenInclude(et => et.Tag)
                 .Include(e => e.Bookings)
                 .Where(expression)
                 .ToListAsync();
        }

        public async Task<bool> HasAvailableSeatsAsync(int eventId, int requestedSeats)
        {
            var eventEntity = await _dbContext.Events
                .Include(e => e.Bookings)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventEntity == null)
                return false;

            var bookedSeats = eventEntity.Bookings.Sum(b => b.TicketCount);
            var availableSeats = eventEntity.AvailableSeats - bookedSeats;

            return availableSeats >= requestedSeats;
        }

        public async Task<IEnumerable<Event>> GetEventsByUserIdAsync(string userId)
        {
            return await _dbContext.Events
                .Where(e => e.CreatedBy == userId)
                .ToListAsync();
        }
    }
}
