using EventBooking.API.Controllers;
using EventBooking.Application.Commands.BookingCommands;
using EventBooking.Application.DTOs;
using EventBooking.Application.Queries.BookingQueries;
using EventBooking.Application.Responses;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Auth.Tests.Controllers
{
    public class BookingsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly BookingsController _controller;

        public BookingsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BookingsController(_mediatorMock.Object);
        }

        private Response<T> CreateResponse<T>(T data, bool succeeded, string message)
        {
            return new Response<T> { Data = data, Succeeded = succeeded, Message = message };
        }

        [Fact]
        public async Task CreateBooking_ValidCommand_ReturnsOk()
        {
            // Arrange
            var command = new CreateBookingCommand { EventId = 1, TicketCount = 2 };
            var response = CreateResponse("Booking Created Successfully", true, "Booking Created Successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBookingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateBooking(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeTrue();
            responseResult.Message.Should().Be("Booking Created Successfully");
        }

        [Fact]
        public async Task CreateBooking_InvalidCommand_ReturnsBadRequest()
        {
            // Arrange
            var command = new CreateBookingCommand { EventId = 1, TicketCount = 2 };
            var response = CreateResponse<string>(null, false, "Not enough tickets available.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateBookingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateBooking(command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            var responseResult = badRequestResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("Not enough tickets available.");
        }

        [Fact]
        public async Task UpdateBooking_ValidCommand_ReturnsOk()
        {
            // Arrange
            var command = new UpdateBookingCommand { Id = 1, EventId = 1, TicketCount = 3 };
            var response = CreateResponse("Booking Updated Successfully", true, "Booking Updated Successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBookingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateBooking(1, command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeTrue();
            responseResult.Message.Should().Be("Booking Updated Successfully");
        }

        [Fact]
        public async Task UpdateBooking_InvalidCommand_ReturnsBadRequest()
        {
            // Arrange
            var command = new UpdateBookingCommand { Id = 1, EventId = 1, TicketCount = 3 };
            var response = CreateResponse<string>(null, false, "Booking not found.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBookingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateBooking(1, command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            var responseResult = badRequestResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("Booking not found.");
        }

        [Fact]
        public async Task DeleteBooking_ValidCommand_ReturnsOk()
        {
            // Arrange
            var response = CreateResponse("Booking Deleted Successfully", true, "Booking Deleted Successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CancelBookingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.DeleteBooking(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeTrue();
            responseResult.Message.Should().Be("Booking Deleted Successfully");
        }

        [Fact]
        public async Task DeleteBooking_InvalidCommand_ReturnsBadRequest()
        {
            // Arrange
            var response = CreateResponse<string>(null, false, "Booking not found.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CancelBookingCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.DeleteBooking(1);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            var responseResult = badRequestResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("Booking not found.");
        }

        [Fact]
        public async Task GetBookingById_ValidId_ReturnsOk()
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                Id = 1,
                UserId = "user1",
                EventId = 1,
                EventName = "Test Event",
                EventDate = DateTime.UtcNow.AddDays(1),
                Venue = "Test Venue",
                Price = 50.00m,
                TicketCount = 2,
                Status = "Confirmed",
                BookingDate = DateTime.UtcNow
            };
            var response = CreateResponse(bookingDto, true, "Booking retrieved successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookingByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetBookingById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<BookingDto>;
            responseResult!.Data.Should().BeEquivalentTo(bookingDto);
            responseResult.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task GetBookingById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var response = CreateResponse<BookingDto>(null, false, "Booking not found.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookingByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetBookingById(1);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            var responseResult = notFoundResult!.Value as Response<BookingDto>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("Booking not found.");
        }

        [Fact]
        public async Task GetAllBookings_ValidQuery_ReturnsOk()
        {
            // Arrange
            var bookings = new List<BookingDto>
            {
                new BookingDto
            {
                Id = 1,
                UserId = "user1",
                EventId = 1,
                EventName = "Test Event",
                EventDate = DateTime.UtcNow.AddDays(1),
                Venue = "Test Venue",
                Price = 50.00m,
                TicketCount = 2,
                Status = "Confirmed",
                BookingDate = DateTime.UtcNow
            },
                new BookingDto
            {
                Id = 1,
                UserId = "user1",
                EventId = 1,
                EventName = "Test Event",
                EventDate = DateTime.UtcNow.AddDays(1),
                Venue = "Test Venue",
                Price = 50.00m,
                TicketCount = 2,
                Status = "Confirmed",
                BookingDate = DateTime.UtcNow
            }
            };
            var response = CreateResponse(bookings, true, "Bookings retrieved successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookingListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllBookings();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<List<BookingDto>>;
            responseResult!.Data.Should().BeEquivalentTo(bookings);
            responseResult.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task GetAllBookings_Error_ReturnsBadRequest()
        {
            // Arrange
            var response = CreateResponse<List<BookingDto>>(null, false, "Error retrieving bookings.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookingListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllBookings();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            var responseResult = badRequestResult!.Value as Response<List<BookingDto>>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("Error retrieving bookings.");
        }

        [Fact]
        public async Task GetBookingsByUser_ValidUserId_ReturnsOk()
        {
            // Arrange
            var bookings = new List<BookingDto>
            {
                new BookingDto
            {
                Id = 1,
                UserId = "user1",
                EventId = 1,
                EventName = "Test Event",
                EventDate = DateTime.UtcNow.AddDays(1),
                Venue = "Test Venue",
                Price = 50.00m,
                TicketCount = 2,
                Status = "Confirmed",
                BookingDate = DateTime.UtcNow
            }
            };
            var response = CreateResponse(bookings, true, "User bookings retrieved successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookingsByUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetBookingsByUser("user1");

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<List<BookingDto>>;
            responseResult!.Data.Should().BeEquivalentTo(bookings);
            responseResult.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task GetBookingsByUser_Unauthorized_ReturnsNotFound()
        {
            // Arrange
            var response = CreateResponse<List<BookingDto>>(null, false, "You are not authorized to view these bookings.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookingsByUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetBookingsByUser("user1");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            var responseResult = notFoundResult!.Value as Response<List<BookingDto>>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("You are not authorized to view these bookings.");
        }
    }
}