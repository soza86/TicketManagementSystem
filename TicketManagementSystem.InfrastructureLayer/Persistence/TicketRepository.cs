using TicketManagementSystem.ApplicationLayer.Interfaces;
using TicketManagementSystem.Core.Entities;

namespace TicketManagementSystem.InfrastructureLayer.Persistence
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(TicketDbContext context) : base(context) { }

        public override async Task<List<Ticket>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<Ticket?> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<Ticket?> GetByNumberAsync(long number)
        {
            return await GetByFieldAsync(f => f.Number == number);
        }

        public override async Task<Ticket> AddAsync(Ticket ticket)
        {
            await base.AddAsync(ticket);
            await SaveChangesAsync();
            return ticket;
        }
    }
}
