namespace EventBooking.Application.DTOs
{
    public record EventDto(string Name, string Description, string Category, DateTime EventDate,
        string Venue, decimal Price, string? ImageUrl, int AvailableTickets, DateTime CreatedAt, DateTime? ModifiedAt);

}
