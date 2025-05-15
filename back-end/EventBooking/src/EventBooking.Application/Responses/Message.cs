namespace EventBooking.Application.Responses
{
    public record Message(IEnumerable<string> To, string Subject, string Body);
}
