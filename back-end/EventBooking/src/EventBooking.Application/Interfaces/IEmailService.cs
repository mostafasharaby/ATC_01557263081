using EventBooking.Application.Responses;

namespace EventBooking.Application.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
