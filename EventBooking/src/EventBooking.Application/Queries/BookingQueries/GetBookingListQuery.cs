using EventBooking.Application.DTOs;
using EventBooking.Application.Responses;
using MediatR;

namespace EventBooking.Application.Queries.BookingQueries
{
    public class GetBookingListQuery : IRequest<Response<List<BookingDto>>>
    {
        public int? EventId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
