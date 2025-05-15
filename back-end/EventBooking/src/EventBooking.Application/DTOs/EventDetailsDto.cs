namespace EventBooking.Application.DTOs
{
    public record EventDetailsDto(int Id, string Name, string Description, string Category,
        DateTime EventDate, string Venue, decimal Price, string? ImageUrl, int AvailableTickets, bool IsBooked,
        DateTime CreatedAt, DateTime? ModifiedAt);

}
