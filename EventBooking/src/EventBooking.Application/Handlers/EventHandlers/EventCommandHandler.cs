using AutoMapper;
using EventBooking.Application.Commands.EventCommands;
using EventBooking.Application.Interfaces;
using EventBooking.Application.Responses;
using EventBooking.Domain.Entities;
using EventBooking.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EventBooking.Application.Handlers.EventHandlers
{
    public class EventCommandHandler : IRequestHandler<CreateEventCommand, Response<string>>,
                                       IRequestHandler<UpdateEventCommand, Response<string>>,
                                       IRequestHandler<DeleteEventCommand, Response<string>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;
        public readonly ResponseHandler _responseHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventCommandHandler(IEventRepository eventRepository, IFileStorageService fileStorageService, IMapper mapper,
            ResponseHandler responseHandler, IHttpContextAccessor httpContextAccessor)
        {
            _eventRepository = eventRepository;
            _fileStorageService = fileStorageService;
            _mapper = mapper;
            _responseHandler = responseHandler;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<string>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                //    ?? throw new UnauthorizedAccessException("User ID not found in token.");

                var ev = _mapper.Map<Event>(request);
                // ev.CreatedBy = userId;
                ev.CreatedAt = DateTime.UtcNow;

                if (request.Image != null)
                {
                    string imageFileName = await _fileStorageService.SaveFileAsync(request.Image, "events");
                    ev.ImageUrl = imageFileName;
                }

                var addedEvent = await _eventRepository.AddAsync(ev);
                return _responseHandler.Created<string>("Event Created Successfully");
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

        public async Task<Response<string>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                //    ?? throw new UnauthorizedAccessException("User ID not found in token.");

                var existingEvent = await _eventRepository.GetByIdAsync(request.Id);
                if (existingEvent == null)
                    return _responseHandler.NotFound<string>("Event not found.");

                //if (existingEvent.CreatedBy != userId)
                //    return _responseHandler.Forbidden<string>("You are not authorized to update this event.");

                _mapper.Map(request, existingEvent);
                existingEvent.UpdatedAt = DateTime.UtcNow;

                if (request.Image != null)
                {
                    if (!string.IsNullOrEmpty(existingEvent.ImageUrl))
                    {
                        await _fileStorageService.DeleteFileAsync(existingEvent.ImageUrl, "events");
                    }

                    string imageFileName = await _fileStorageService.SaveFileAsync(request.Image, "events");
                    existingEvent.ImageUrl = imageFileName;
                }
                else if (request.RemoveCurrentImage && !string.IsNullOrEmpty(existingEvent.ImageUrl))
                {
                    await _fileStorageService.DeleteFileAsync(existingEvent.ImageUrl, "events");
                    existingEvent.ImageUrl = null;
                }

                await _eventRepository.UpdateAsync(existingEvent);
                return _responseHandler.Success<string>("Event Updated Successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                return _responseHandler.Unauthorized<string>(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return _responseHandler.NotFound<string>(ex.Message);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<string>(ex.Message);
            }
        }

        public async Task<Response<string>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                //    ?? throw new UnauthorizedAccessException("User ID not found in token.");

                var existingEvent = await _eventRepository.GetByIdAsync(request.Id);
                if (existingEvent == null)
                    return _responseHandler.NotFound<string>("Event not found.");

                //if (existingEvent.CreatedBy != userId)
                //    return _responseHandler.Forbidden<string>("You are not authorized to delete this event.");

                if (!string.IsNullOrEmpty(existingEvent.ImageUrl))
                {
                    await _fileStorageService.DeleteFileAsync(existingEvent.ImageUrl, "events");
                }

                await _eventRepository.DeleteByIdAsync(request.Id);
                return _responseHandler.Success<string>("Event Deleted Successfully");
            }
            catch (UnauthorizedAccessException ex)
            {
                return _responseHandler.Unauthorized<string>(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return _responseHandler.NotFound<string>(ex.Message);
            }
            catch (Exception ex)
            {
                return _responseHandler.BadRequest<string>(ex.Message);
            }
        }
    }
}
