using TicketManagementSystem.ApplicationLayer.DTOs;
using TicketManagementSystem.Core.Entities;

namespace TicketManagementSystem.ApplicationLayer.Interfaces
{
    public interface ITicketMapper
    {
        TicketDto MapToTicketDto(Ticket ticket);

        Ticket MapToTicket(CreateTicketDto createTicketDto);
    }
}
