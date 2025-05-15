using AutoMapper;
using EventBooking.Application.Commands.BookingCommands;
using EventBooking.Application.Responses;
using EventBooking.Domain.Entities;
using EventBooking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EventBooking.Application.Handlers.BookingHandlers
{

    public class BookingCommandHandler : IRequestHandler<CreateBookingCommand, Response<string>>,
                                         IRequestHandler<UpdateBookingCommand, Response<string>>,
                                         IRequestHandler<CancelBookingCommand, Response<string>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingCommandHandler(IBookingRepository bookingRepository, IEventRepository eventRepository, IMapper mapper,
            ResponseHandler responseHandler, IHttpContextAccessor httpContextAccessor)
        {
            _bookingRepository = bookingRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<string>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new UnauthorizedAccessException("User ID not found in token.");

                var eventItem = await _eventRepository.GetByIdAsync(request.EventId);
                if (eventItem == null)
                    return _responseHandler.NotFound<string>("Event not found.");

                var hasSeats = await _bookingRepository.HasAvailableSeatsAsync(request.EventId, request.TicketCount);
                if (!hasSeats)
                    return _responseHandler.BadRequest<string>("Not enough tickets available.");

                var booking = _mapper.Map<Booking>(request);
                booking.UserId = userId;
                booking.BookingDate = DateTime.UtcNow;

                eventItem.AvailableSeats -= request.TicketCount;
                await _eventRepository.UpdateAsync(eventItem);

                await _bookingRepository.AddAsync(booking);
                return _responseHandler.Created<string>("Booking Created Successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                return _responseHandler.Unauthorized<string>(ex.Message);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<string>(ex.Message);
            }
        }


        public async Task<Response<string>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new UnauthorizedAccessException("User ID not found in token.");

                var existingBooking = await _bookingRepository.GetByIdAsync(request.Id);
                if (existingBooking == null)
                    return _responseHandler.NotFound<string>("Booking not found.");

                if (existingBooking.UserId != userId)
                    return _responseHandler.Forbidden<string>("You are not authorized to update this booking.");

                var eventItem = await _eventRepository.GetByIdAsync(request.EventId);
                if (eventItem == null)
                    return _responseHandler.NotFound<string>("Event not found.");

                var ticketDifference = request.TicketCount - existingBooking.TicketCount;
                if (ticketDifference > 0)
                {
                    var hasSeats = await _bookingRepository.HasAvailableSeatsAsync(request.EventId, ticketDifference);
                    if (!hasSeats)
                        return _responseHandler.BadRequest<string>("Not enough tickets available.");
                }

                eventItem.AvailableSeats -= ticketDifference;
                await _eventRepository.UpdateAsync(eventItem);

                _mapper.Map(request, existingBooking);
                await _bookingRepository.UpdateAsync(existingBooking);

                return _responseHandler.Success<string>("Booking Updated Successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                return _responseHandler.Unauthorized<string>(ex.Message);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<string>(ex.Message);
            }
        }


        public async Task<Response<string>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new UnauthorizedAccessException("User ID not found in token.");

                var existingBooking = await _bookingRepository.GetByIdAsync(request.Id);
                if (existingBooking == null)
                    return _responseHandler.NotFound<string>("Booking not found.");

                if (existingBooking.UserId != userId)
                    return _responseHandler.Forbidden<string>("You are not authorized to delete this booking.");

                var eventItem = await _eventRepository.GetByIdAsync(existingBooking.EventId);
                if (eventItem != null)
                {
                    eventItem.AvailableSeats += existingBooking.TicketCount;
                    await _eventRepository.UpdateAsync(eventItem);
                }

                await _bookingRepository.DeleteByIdAsync(request.Id);
                return _responseHandler.Success<string>("Booking Deleted Successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                return _responseHandler.Unauthorized<string>(ex.Message);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<string>(ex.Message);
            }
        }
    }
}
