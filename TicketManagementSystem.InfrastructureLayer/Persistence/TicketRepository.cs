using TicketManagementSystem.ApplicationLayer.Interfaces;
using TicketManagementSystem.Core.Entities;

namespace TicketManagementSystem.InfrastructureLayer.Persistence
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketDbContext context) : base(context) { }

        public Task<List<Ticket?>> GetAllAsync()
        {
            return base.GetAllAsync();
        }

        public Task<Ticket?> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }

        public Task<Ticket?> GetByNumberAsync(int number)
        {
            return GetByFieldAsync(f => f.Number == number);
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            await base.AddAsync(ticket);
            await SaveChangesAsync();
            return ticket;
        }
    }
}
