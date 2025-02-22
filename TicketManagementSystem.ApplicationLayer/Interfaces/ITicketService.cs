using TicketManagementSystem.ApplicationLayer.DTOs;

namespace TicketManagementSystem.ApplicationLayer.Interfaces
{
    public interface ITicketService
    {
        Task<ServiceResponse<List<TicketDto>>> GetAllAsync();

        Task<ServiceResponse<TicketDto?>> GetByNumberAsync(int number);

        Task<ServiceResponse<TicketDto?>> CreateAsync(CreateTicketDto createTicketDto);
    }
}
