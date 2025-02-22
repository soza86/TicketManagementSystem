using FluentValidation;
using TicketManagementSystem.ApplicationLayer.DTOs;

namespace TicketManagementSystem.API.Validators
{
    public class CreateTicketDtoValidator : AbstractValidator<CreateTicketDto>
    {
        public CreateTicketDtoValidator()
        {
            RuleFor(x => x.Number).NotEmpty().WithMessage("Ticket number is required");
            RuleFor(x => x.FlightDate).NotEmpty().WithMessage("Flight date is required")
                                      .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Flight date can not be in the past.");
            RuleFor(x => x.DepartureCity).NotEmpty().WithMessage("Departure city is required")
                                         .NotEqual(x => x.ArrivalCity).WithMessage("Departure city can not be the same as arrival city");
            RuleFor(x => x.DepartureCountry).NotEmpty().WithMessage("Departure country is required");
            RuleFor(x => x.ArrivalCity).NotEmpty().WithMessage("Arrival city is required")
                                       .NotEqual(x => x.DepartureCity).WithMessage("Arrival city can not be the same as departure city");
            RuleFor(x => x.ArrivalCountry).NotEmpty().WithMessage("Arrival country is required");
        }
    }
}
