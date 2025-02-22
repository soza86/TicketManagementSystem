using TicketManagementSystem.Core.Entities;

namespace TicketManagementSystem.ApplicationLayer.Interfaces
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllAsync();

        Task<Ticket?> GetByIdAsync(int id);

        Task<Ticket?> GetByNumberAsync(long number);

        Task<Ticket> AddAsync(Ticket ticket);
    }
}
