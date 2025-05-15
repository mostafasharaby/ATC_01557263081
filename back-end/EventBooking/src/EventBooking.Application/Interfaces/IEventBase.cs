namespace EventBooking.Application.Interfaces
{
    public interface IEventBase
    {
        string Name { get; }
        string Description { get; }
        string Category { get; }
        DateTime EventDate { get; }
        string Venue { get; }
        decimal Price { get; }
        int AvailableTickets { get; }
    }
}
