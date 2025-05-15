namespace EventBooking.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser? User { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public int TicketCount { get; set; }
        public DateTime? BookingDate { get; set; }
    }
}
