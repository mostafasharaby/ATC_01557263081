namespace EventBooking.Domain.Entities
{
    public class Event : BaseEntity
    {
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public DateTime EventDate { get; set; }
        public string? Venue { get; set; }
        public decimal? Price { get; set; }
        public string? ImageUrl { get; set; }
        public int AvailableSeats { get; set; }
        public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
        public ICollection<EventTag>? EventTags { get; set; } = new List<EventTag>();
    }
}
