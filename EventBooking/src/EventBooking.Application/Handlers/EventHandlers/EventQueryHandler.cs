using AutoMapper;
using EventBooking.Application.DTOs;
using EventBooking.Application.Queries.EventQueries;
using EventBooking.Application.Responses;
using EventBooking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EventBooking.Application.Handlers.EventHandlers
{
    public class EventQueryHandler : IRequestHandler<GetEventByIdQuery, Response<EventDetailsDto>>,
                                     IRequestHandler<GetEventListQuery, Response<List<EventDto>>>,
                                     IRequestHandler<GetEventsByUserQuery, Response<List<EventDetailsDto>>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public readonly ResponseHandler _responseHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventQueryHandler(
            IEventRepository eventRepository,
            ResponseHandler responseHandler,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<EventDetailsDto>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var eventId = request.Id;
                var eventItem = await _eventRepository.GetByIdAsync(eventId);

                if (eventItem == null)
                    return _responseHandler.NotFound<EventDetailsDto>("Event not found.");

                var eventDto = _mapper.Map<EventDetailsDto>(eventItem);
                return _responseHandler.Success(eventDto);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<EventDetailsDto>(ex.Message);
            }
        }

        public async Task<Response<List<EventDto>>> Handle(GetEventListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var events = await _eventRepository.GetAllAsync();
                //request.SearchTerm,
                //request.Category,
                //request.FromDate,
                //request.ToDate,
                //request.PageNumber,
                //request.PageSize);

                var eventsDto = _mapper.Map<List<EventDto>>(events);
                return _responseHandler.Success(eventsDto);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<List<EventDto>>(ex.Message);
            }
        }

        public async Task<Response<List<EventDetailsDto>>> Handle(GetEventsByUserQuery request, CancellationToken cancellationToken)
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

                    if (!isAdmin && authenticatedUserId != userId.ToString())
                    {
                        return _responseHandler.Forbidden<List<EventDetailsDto>>("You are not authorized to view these events.");
                    }
                }
                var events = await _eventRepository.GetEventsByUserIdAsync(userId);
                var eventsDto = _mapper.Map<List<EventDetailsDto>>(events);
                return _responseHandler.Success(eventsDto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return _responseHandler.Unauthorized<List<EventDetailsDto>>(ex.Message);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<List<EventDetailsDto>>(ex.Message);
            }
        }
    }
}