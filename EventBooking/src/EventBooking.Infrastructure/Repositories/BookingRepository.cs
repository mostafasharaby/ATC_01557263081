using EventBooking.Domain.Entities;
using EventBooking.Domain.Repositories;
using EventBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventBooking.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AppDbContext context) : base(context) { }

        public async override Task<Booking> GetByIdAsync(int id)
        {
            return await _dbContext.Bookings
                .Include(b => b.Event)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async override Task<List<Booking>> GetAllAsync()
        {
            return await _dbContext.Bookings
                .Include(b => b.Event)
                .ToListAsync();
        }

        public async override Task<List<Booking>> GetByAsync(Expression<Func<Booking, bool>> expression)
        {
            return await _dbContext.Bookings
                .Include(b => b.Event)
                .Where(expression)
                .ToListAsync();
        }

        public Task<bool> HasAvailableSeatsAsync(int eventId, int requestedSeats)
        {
            throw new NotImplementedException();
        }
    }

}