using EventBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #region DbSets
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<EventTag> EventTags { get; set; }
        #endregion

        #region Model Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureEventTag(modelBuilder);
            ConfigureBooking(modelBuilder);
            ConfigureEvent(modelBuilder);
            SeedRoles(modelBuilder);
            SeedUsers(modelBuilder);
            SeedUserRoles(modelBuilder);
            SeedTags(modelBuilder);
            SeedEvents(modelBuilder);
            SeedEventTags(modelBuilder);
            SeedBookings(modelBuilder);
        }

        private static void ConfigureEventTag(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventTag>()
                .HasKey(et => new { et.EventId, et.TagId });

            modelBuilder.Entity<EventTag>()
                .HasOne(et => et.Event)
                .WithMany(e => e.EventTags)
                .HasForeignKey(et => et.EventId);

            modelBuilder.Entity<EventTag>()
                .HasOne(et => et.Tag)
                .WithMany(t => t.EventTags)
                .HasForeignKey(et => et.TagId);
        }

        private static void ConfigureBooking(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);
        }

        private static void ConfigureEvent(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .Property(e => e.ImageUrl)
                .HasMaxLength(500);

            modelBuilder.Entity<Event>()
                .Property(e => e.Price)
                .HasPrecision(10, 2);
        }
        #endregion

        #region Seed Data
        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "roleId1", Name = "admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "roleId2", Name = "user", NormalizedName = "USER" }
            );
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "user1",
                    UserName = "Shady",
                    Email = "Shady@example.com",
                    NormalizedUserName = "SHADY",
                    NormalizedEmail = "SHADY@EXAMPLE.COM"
                },
                new AppUser
                {
                    Id = "user2",
                    UserName = "Mohamed",
                    Email = "Mohamed@example.com",
                    NormalizedUserName = "MOHAMED",
                    NormalizedEmail = "MOHAMED@EXAMPLE.COM"
                },
                new AppUser
                {
                    Id = "admin1",
                    UserName = "admin",
                    Email = "admin@example.com",
                    NormalizedUserName = "ADMIN",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM"
                }
            );
        }

        private static void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "admin1", RoleId = "roleId1" },
                new IdentityUserRole<string> { UserId = "user1", RoleId = "roleId2" },
                new IdentityUserRole<string> { UserId = "user2", RoleId = "roleId2" }
            );
        }

        private static void SeedTags(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tag>().HasData(
                new Tag { Id = 6, Name = "Music", CreatedAt = DateTime.UtcNow },
                new Tag { Id = 7, Name = "Conference", CreatedAt = DateTime.UtcNow },
                new Tag { Id = 8, Name = "Workshop", CreatedAt = DateTime.UtcNow }
            );
        }

        private static void SeedEvents(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 8,
                    Name = "Champions League Final",
                    CreatedBy = "user1",
                    Description = "The biggest football showdown of the year.",
                    Category = "Football",
                    EventDate = DateTime.UtcNow.AddDays(15),
                    Venue = "National Stadium",
                    Price = 99.99m,
                    ImageUrl = "/images/events/football.webp",
                    AvailableSeats = 50000,
                    CreatedAt = DateTime.UtcNow
                },
                new Event
                {
                    Id = 9,
                    Name = "NBA All-Star Game",
                    CreatedBy = "user2",
                    Description = "Experience the thrill of top NBA talent in one spectacular game.",
                    Category = "Basketball",
                    EventDate = DateTime.UtcNow.AddDays(30),
                    Venue = "Madison Square Garden",
                    Price = 149.99m,
                    ImageUrl = "/images/events/basketball.jpeg",
                    AvailableSeats = 20000,
                    CreatedAt = DateTime.UtcNow
                },
                new Event
                {
                    Id = 10,
                    Name = "Gourmet Food Expo",
                    CreatedBy = "user1",
                    Description = "A celebration of world cuisine and culinary innovation.",
                    Category = "Food",
                    EventDate = DateTime.UtcNow.AddDays(20),
                    Venue = "City Exhibition Hall",
                    Price = 25.00m,
                    ImageUrl = "/images/events/food.jpg",
                    AvailableSeats = 1000,
                    CreatedAt = DateTime.UtcNow
                },
                new Event
                {
                    Id = 11,
                    Name = "Live Concert: The Soundwave Tour",
                    CreatedBy = "user2",
                    Description = "Join the electrifying concert experience with top artists live.",
                    Category = "Concert",
                    EventDate = DateTime.UtcNow.AddDays(45),
                    Venue = "Open Air Arena",
                    Price = 79.99m,
                    ImageUrl = "/images/events/concert.jpeg",
                    AvailableSeats = 10000,
                    CreatedAt = DateTime.UtcNow
                }
            );

        }

        private static void SeedEventTags(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventTag>().HasData(
                new EventTag { EventId = 8, TagId = 6 },
                new EventTag { EventId = 9, TagId = 7 },
                new EventTag { EventId = 10, TagId = 8 }
            );
        }

        private static void SeedBookings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 8,
                    UserId = "user1",
                    EventId = 10,
                    TicketCount = 2,
                    BookingDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                },
                new Booking
                {
                    Id = 9,
                    UserId = "user2",
                    EventId = 11,
                    TicketCount = 1,
                    BookingDate = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
        #endregion

        #region SaveChanges
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
