using EventBooking.Application.Commands.BookingCommands;
using EventBooking.Application.Interfaces;
using FluentValidation;

namespace EventBooking.Application.Validators.BookingValidators
{
    public class BookingCommandValidator<T> : AbstractValidator<T> where T : IBookingBase
    {
        public BookingCommandValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Event ID must be a valid positive number.");

            RuleFor(x => x.TicketCount)
                .GreaterThan(0).WithMessage("Ticket count must be at least 1.")
                .LessThanOrEqualTo(10).WithMessage("Cannot book more than 10 tickets at once.");
        }
    }

    public class CreateBookingCommandValidator : BookingCommandValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator() : base() { }
    }

    public class UpdateBookingCommandValidator : BookingCommandValidator<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Booking ID must be a valid positive number.");
        }
    }
}
