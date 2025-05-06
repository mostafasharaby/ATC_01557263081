using EventBooking.API.Controllers;
using EventBooking.Application.Commands.EventCommands;
using EventBooking.Application.DTOs;
using EventBooking.Application.Queries.EventQueries;
using EventBooking.Application.Responses;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EventBooking.Tests.Controllers
{
    public class EventsControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly EventsController _controller;

        public EventsControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new EventsController(_mediatorMock.Object);
        }

        private Response<T> CreateResponse<T>(T data, bool succeeded, string message)
        {
            return new Response<T> { Data = data, Succeeded = succeeded, Message = message };
        }

        [Fact]
        public async Task CreateEvent_ValidCommand_ReturnsOk()
        {
            // Arrange
            var command = new CreateEventCommand { Name = "Test Event", Description = "Description", EventDate = DateTime.UtcNow.AddDays(1) };
            var response = CreateResponse("Event Created Successfully", true, "Event Created Successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateEventCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateEvent(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeTrue();
            responseResult.Message.Should().Be("Event Created Successfully");
        }

        [Fact]
        public async Task CreateEvent_InvalidCommand_ReturnsBadRequest()
        {
            // Arrange
            var command = new CreateEventCommand { Name = "Test Event", Description = "Description", EventDate = DateTime.UtcNow.AddDays(1) };
            var response = CreateResponse<string>(null, false, "Unauthorized access.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateEventCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateEvent(command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            var responseResult = badRequestResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("Unauthorized access.");
        }

        [Fact]
        public async Task UpdateEvent_ValidCommand_ReturnsOk()
        {
            // Arrange
            var command = new UpdateEventCommand { Id = 1, Name = "Updated Event", Description = "Updated Description" };
            var response = CreateResponse("Event Updated Successfully", true, "Event Updated Successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateEventCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateEvent(1, command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeTrue();
            responseResult.Message.Should().Be("Event Updated Successfully");
        }

        [Fact]
        public async Task UpdateEvent_InvalidCommand_ReturnsBadRequest()
        {
            // Arrange
            var command = new UpdateEventCommand { Id = 1, Name = "Updated Event", Description = "Updated Description" };
            var response = CreateResponse<string>(null, false, "Event not found.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateEventCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateEvent(1, command);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            var responseResult = badRequestResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("Event not found.");
        }

        [Fact]
        public async Task DeleteEvent_ValidCommand_ReturnsOk()
        {
            // Arrange
            var response = CreateResponse("Event Deleted Successfully", true, "Event Deleted Successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.DeleteEvent(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeTrue();
            responseResult.Message.Should().Be("Event Deleted Successfully");
        }

        [Fact]
        public async Task DeleteEvent_InvalidCommand_ReturnsBadRequest()
        {
            // Arrange
            var response = CreateResponse<string>(null, false, "Event not found.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteEventCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.DeleteEvent(1);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            var responseResult = badRequestResult!.Value as Response<string>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("Event not found.");
        }

        [Fact]
        public async Task GetEventById_ValidId_ReturnsOk()
        {
            // Arrange
            var eventDto = new EventDto(
                    Name: "Test Event 1",
                    Description: "Description 1",
                    Category: "Concert",
                    EventDate: DateTime.UtcNow.AddDays(1),
                    Venue: "Test Venue 1",
                    Price: 50.00m,
                    ImageUrl: "test1.jpg",
                    AvailableTickets: 100,
                    CreatedAt: DateTime.UtcNow,
                    ModifiedAt: null
                );

            var response = CreateResponse(eventDto, true, "Event retrieved successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetEventById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<EventDetailsDto>;
            responseResult!.Data.Should().BeEquivalentTo(eventDto);
            responseResult.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task GetEventById_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var response = CreateResponse<EventDto>(null, false, "Event not found.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetEventById(1);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            var responseResult = notFoundResult!.Value as Response<EventDetailsDto>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("Event not found.");
        }

        [Fact]
        public async Task GetAllEvents_ValidQuery_ReturnsOk()
        {
            // Arrange
            var events = new List<EventDto>
            {
                new EventDto(
                    Name: "Test Event 1",
                    Description: "Description 1",
                    Category: "Concert",
                    EventDate: DateTime.UtcNow.AddDays(1),
                    Venue: "Test Venue 1",
                    Price: 50.00m,
                    ImageUrl: "test1.jpg",
                    AvailableTickets: 100,
                    CreatedAt: DateTime.UtcNow,
                    ModifiedAt: null
                ),
                new EventDto(
                    Name: "Test Event 2",
                    Description: "Description 2",
                    Category: "Conference",
                    EventDate: DateTime.UtcNow.AddDays(2),
                    Venue: "Test Venue 2",
                    Price: 75.00m,
                    ImageUrl: "test2.jpg",
                    AvailableTickets: 200,
                    CreatedAt: DateTime.UtcNow,
                    ModifiedAt: null
                )
            };
            var response = CreateResponse(events, true, "Events retrieved successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllEvents();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<List<EventDto>>;
            responseResult!.Data.Should().BeEquivalentTo(events);
            responseResult.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task GetAllEvents_Error_ReturnsBadRequest()
        {
            // Arrange
            var response = CreateResponse<List<EventDto>>(null, false, "Error retrieving events.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventListQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetAllEvents();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            var badRequestResult = result as BadRequestObjectResult;
            var responseResult = badRequestResult!.Value as Response<List<EventDto>>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("Error retrieving events.");
        }

        [Fact]
        public async Task GetEventsByUserId_ValidUserId_ReturnsOk()
        {
            // Arrange
            var events = new List<EventDto>
            {
                new EventDto(
                    Name: "Test Event 1",
                    Description: "Description 1",
                    Category: "Concert",
                    EventDate: DateTime.UtcNow.AddDays(1),
                    Venue: "Test Venue 1",
                    Price: 50.00m,
                    ImageUrl: "test1.jpg",
                    AvailableTickets: 100,
                    CreatedAt: DateTime.UtcNow,
                    ModifiedAt: null
                )
            };
            var response = CreateResponse(events, true, "User events retrieved successfully");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventsByUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetEventsByUserId("user1");

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var responseResult = okResult!.Value as Response<List<EventDetailsDto>>;
            responseResult!.Data.Should().BeEquivalentTo(events);
            responseResult.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task GetEventsByUserId_Unauthorized_ReturnsNotFound()
        {
            // Arrange
            var response = CreateResponse<List<EventDto>>(null, false, "You are not authorized to view these events.");
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetEventsByUserQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetEventsByUserId("user1");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            var notFoundResult = result as NotFoundObjectResult;
            var responseResult = notFoundResult!.Value as Response<List<EventDetailsDto>>;
            responseResult!.Succeeded.Should().BeFalse();
            responseResult.Message.Should().Be("You are not authorized to view these events.");
        }
    }
}
