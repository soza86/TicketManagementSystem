using TicketManagementSystem.ApplicationLayer.DTOs;
using TicketManagementSystem.ApplicationLayer.Interfaces;
using TicketManagementSystem.Core.Entities;
using TicketManagementSystem.Core.Interfaces;

namespace TicketManagementSystem.ApplicationLayer.Services
{
    public class TicketService(IGenericRepository<Ticket> ticketRepositoy) : ITicketService
    {
        private readonly IGenericRepository<Ticket> _ticketRepositoy = ticketRepositoy;

        public async Task<ServiceResponse<List<TicketDto>>> GetAllAsync()
        {
            var response = await _ticketRepositoy.GetAllAsync();
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TicketDto?>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<TicketDto>> CreateAsync(CreateTicketDto ticket)
        {
            throw new NotImplementedException();
        }
    }
}
