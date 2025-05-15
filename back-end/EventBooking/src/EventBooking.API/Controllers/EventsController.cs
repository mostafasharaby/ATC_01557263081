using EventBooking.Application.Commands.EventCommands;
using EventBooking.Application.Queries.EventQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromForm] CreateEventCommand command)
        {
            var result = await _mediator.Send(command);
            return (bool)result.Succeeded! ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromForm] UpdateEventCommand command)
        {
            //command.Id = id;
            var result = await _mediator.Send(command);
            return (bool)result.Succeeded! ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var command = new DeleteEventCommand(id);
            var result = await _mediator.Send(command);
            return (bool)result.Succeeded! ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var query = new GetEventByIdQuery(id);
            var result = await _mediator.Send(query);
            return (bool)result.Succeeded! ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var query = new GetEventListQuery();
            var result = await _mediator.Send(query);
            return (bool)result.Succeeded! ? Ok(result) : BadRequest(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetEventsByUserId(string userId)
        {
            var query = new GetEventsByUserQuery { UserId = userId };
            var result = await _mediator.Send(query);
            return (bool)result.Succeeded! ? Ok(result) : NotFound(result);
        }
    }
}