namespace EventBooking.Application.Interfaces
{
    public interface IBookingBase
    {
        int EventId { get; set; }
        int TicketCount { get; set; }
    }
}
