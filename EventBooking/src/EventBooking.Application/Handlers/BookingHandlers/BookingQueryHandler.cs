using AutoMapper;
using EventBooking.Application.DTOs;
using EventBooking.Application.Queries.BookingQueries;
using EventBooking.Application.Responses;
using EventBooking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EventBooking.Application.Handlers.BookingHandlers
{

    public class BookingQueryHandler : IRequestHandler<GetBookingByIdQuery, Response<BookingDto>>,
                                       IRequestHandler<GetBookingListQuery, Response<List<BookingDto>>>,
                                       IRequestHandler<GetBookingsByUserQuery, Response<List<BookingDto>>>,
                                       IRequestHandler<GetTotalEarningsQuery, Response<decimal>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingQueryHandler(IBookingRepository bookingRepository, IMapper mapper, ResponseHandler responseHandler, IHttpContextAccessor httpContextAccessor)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<BookingDto>> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var booking = await _bookingRepository.GetByIdAsync(request.Id);
                if (booking == null)
                    return _responseHandler.NotFound<BookingDto>("Booking not found.");

                var bookingDto = _mapper.Map<BookingDto>(booking);
                return _responseHandler.Success(bookingDto);
            }
            catch (FormatException)
            {
                return _responseHandler.BadRequest<BookingDto>("Invalid booking ID format.");
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<BookingDto>(ex.Message);
            }
        }

        public async Task<Response<List<BookingDto>>> Handle(GetBookingListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookings = await _bookingRepository.GetAllAsync();
                if (request.EventId.HasValue)
                {
                    bookings = bookings.Where(b => b.EventId == request.EventId.Value).ToList();
                }

                var pagedBookings = bookings
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var bookingsDto = _mapper.Map<List<BookingDto>>(pagedBookings);
                return _responseHandler.Success(bookingsDto);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<List<BookingDto>>(ex.Message);
            }
        }

        public async Task<Response<List<BookingDto>>> Handle(GetBookingsByUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                string userId = "";

                if (request.UserId == null)
                {
                    var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? throw new UnauthorizedAccessException("User ID not found in token.");
                    userId = authenticatedUserId;
                }
                else
                {
                    userId = request.UserId;
                    var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var isAdmin = _httpContextAccessor.HttpContext?.User.IsInRole("Admin") ?? false;

                    if (!isAdmin && authenticatedUserId != userId)
                    {
                        return _responseHandler.Forbidden<List<BookingDto>>("You are not authorized to view these bookings.");
                    }
                }

                var bookings = await _bookingRepository.GetBookingsByUserIdAsync(userId);
                var bookingsDto = _mapper.Map<List<BookingDto>>(bookings);
                return _responseHandler.Success(bookingsDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return _responseHandler.Unauthorized<List<BookingDto>>(ex.Message);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<List<BookingDto>>(ex.Message);
            }
        }
        public async Task<Response<decimal>> Handle(GetTotalEarningsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookings = await _bookingRepository.GetAllAsync();
                var bookingsDto = _mapper.Map<List<BookingDto>>(bookings);

                var totalEarnings = bookingsDto
                    .Where(b => b.Price.HasValue)
                    .Sum(b => b.Price.Value * b.TicketCount);

                return _responseHandler.Success(totalEarnings);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<decimal>(ex.Message);
            }
        }
    }
}
