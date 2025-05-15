using EventBooking.Domain.Entities;
using EventBooking.Domain.Repositories;
using EventBooking.Infrastructure.Data;

namespace EventBooking.Infrastructure.Repositories
{
    internal class EventTagRepositor : GenericRepository<EventTag>, IEventTagRepositor
    {
        public EventTagRepositor(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
