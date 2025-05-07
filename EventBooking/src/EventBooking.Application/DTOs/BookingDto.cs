namespace EventBooking.Application.DTOs
{
    public record BookingDto
    {
        public int Id { get; init; }
        public string? UserId { get; init; }
        public int? EventId { get; init; }
        public string? EventName { get; init; }
        public DateTime EventDate { get; init; }
        public string? Venue { get; init; }
        public decimal? Price { get; init; }
        public int TicketCount { get; init; }
        public string? Status { get; init; }
        public DateTime? BookingDate { get; init; }
    }
}
