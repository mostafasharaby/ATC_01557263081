namespace EventBooking.Application.DTOs
{
    public record BookingDto(int Id, string UserId, int EventId, string EventName, DateTime EventDate,
        string Venue, decimal Price, int TicketCount, string Status, DateTime BookingDate);
}
