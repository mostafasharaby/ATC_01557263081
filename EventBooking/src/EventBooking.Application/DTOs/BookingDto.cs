namespace EventBooking.Application.DTOs
{
    public record BookingDto(int Id, string UserId, Guid EventId, string EventName, DateTime EventDate,
        string Venue, decimal Price, int TicketCount, string Status, DateTime BookingDate);
}
