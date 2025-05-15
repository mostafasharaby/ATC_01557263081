using EventBooking.Domain.Entities;
using EventBooking.Domain.Repositories;
using EventBooking.Infrastructure.Data;

namespace EventBooking.Infrastructure.Repositories
{
    internal class TagRepository : GenericRepository<EventTag>, ITagRepository
    {
        public TagRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
