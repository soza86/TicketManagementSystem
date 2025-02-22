using TicketManagementSystem.ApplicationLayer.DTOs;
using TicketManagementSystem.ApplicationLayer.Interfaces;
using TicketManagementSystem.Core.Entities;

namespace TicketManagementSystem.ApplicationLayer.Common
{
    public class TicketMapper : ITicketMapper
    {
        public TicketDto MapToTicketDto(Ticket ticket)
        {
            return new TicketDto
            {
                Id = ticket.Id,
                Number = ticket.Number,
                FlightDate = ticket.FlightDate,
                DepartureCity = ticket.DepartureCity,
                DepartureCountry = ticket.DepartureCountry,
                ArrivalCity = ticket.ArrivalCity,
                ArrivalCountry = ticket.ArrivalCountry,
            };
        }

        public Ticket MapToTicket(CreateTicketDto createTicketDto)
        {
            return new Ticket
            {
                Number = createTicketDto.Number,
                FlightDate = createTicketDto.FlightDate,
                DepartureCity = createTicketDto.DepartureCity,
                DepartureCountry = createTicketDto.DepartureCountry,
                ArrivalCity = createTicketDto.ArrivalCity,
                ArrivalCountry = createTicketDto.ArrivalCountry,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow,
            };
        }
    }
}
