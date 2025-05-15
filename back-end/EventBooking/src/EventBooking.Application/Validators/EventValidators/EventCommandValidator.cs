using EventBooking.Application.Commands.EventCommands;
using EventBooking.Application.Interfaces;
using FluentValidation;

namespace EventBooking.Application.Validators.EventValidators
{
    public class EventCommandValidator<T> : AbstractValidator<T> where T : IEventBase
    {
        public EventCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Event name is required.")
                .MaximumLength(100).WithMessage("Event name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.");

            RuleFor(x => x.EventDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Event date must be in the future.");

            RuleFor(x => x.Venue)
                .NotEmpty().WithMessage("Venue is required.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");

            RuleFor(x => x.AvailableTickets)
                .GreaterThanOrEqualTo(0).WithMessage("Available tickets cannot be negative.");
        }
    }

    public class CreateEventCommandValidator : EventCommandValidator<CreateEventCommand>
    {
        public CreateEventCommandValidator() : base() { }
    }

    public class UpdateEventCommandValidator : EventCommandValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Event ID must be a valid positive number.");
        }
    }
}
