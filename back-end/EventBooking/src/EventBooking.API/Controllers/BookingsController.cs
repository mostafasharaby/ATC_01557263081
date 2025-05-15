using EventBooking.Application.Commands.BookingCommands;
using EventBooking.Application.Queries.BookingQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //  [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand command)
        {
            var result = await _mediator.Send(command);
            return (bool)result.Succeeded! ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] UpdateBookingCommand command)
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            return (bool)result.Succeeded! ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var command = new CancelBookingCommand(id);
            var result = await _mediator.Send(command);
            return (bool)result.Succeeded! ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var query = new GetBookingByIdQuery(id);
            var result = await _mediator.Send(query);
            return (bool)result.Succeeded! ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookings()
        {
            var query = new GetBookingListQuery();
            var result = await _mediator.Send(query);
            return (bool)result.Succeeded! ? Ok(result) : BadRequest(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookingsByUser(string userId)
        {
            var query = new GetBookingsByUserQuery(userId);
            var result = await _mediator.Send(query);
            return (bool)result.Succeeded! ? Ok(result) : NotFound(result);
        }

        [HttpGet("total-earnings")]
        public async Task<IActionResult> GetTotalEarnings()
        {
            var query = new GetTotalEarningsQuery();
            var result = await _mediator.Send(query);

            return (bool)result.Succeeded! ? Ok(result) : BadRequest(result);
        }
    }
}
