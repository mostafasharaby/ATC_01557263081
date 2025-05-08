using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Queries.BookingQueries
{
    public class GetTotalEarningsQuery : IRequest<Response<decimal>>
    {
    }
}
