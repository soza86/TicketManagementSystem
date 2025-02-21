using TicketManagementSystem.ApplicationLayer.DTOs;

namespace TicketManagementSystem.ApplicationLayer.Interfaces
{
    public interface ITicketService
    {
        Task<ServiceResponse<List<TicketDto>>> GetAllAsync();

        Task<ServiceResponse<TicketDto?>> GetByIdAsync(int id);

        Task<ServiceResponse<TicketDto>> CreateAsync(CreateTicketDto ticket);
    }
}
