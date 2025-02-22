using TicketManagementSystem.ApplicationLayer.DTOs;
using TicketManagementSystem.ApplicationLayer.Interfaces;

namespace TicketManagementSystem.ApplicationLayer.Services
{
    public class TicketService(ITicketRepository ticketRepositoy, ITicketMapper ticketMapper) : ITicketService
    {
        private readonly ITicketRepository _ticketRepositoy = ticketRepositoy;
        private readonly ITicketMapper _ticketMapper = ticketMapper;

        public async Task<ServiceResponse<List<TicketDto>>> GetAllAsync()
        {
            var response = await _ticketRepositoy.GetAllAsync();
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<TicketDto?>> GetByIdAsync(int id)
        {
            var ticketRecord = await _ticketRepositoy.GetByIdAsync(id);

            if (ticketRecord == null)
                return ServiceResponse<TicketDto?>.NotFoundResponse("Ticket not found.");

            var ticketDto = _ticketMapper.MapToTicketDto(ticketRecord);
            return ServiceResponse<TicketDto?>.SuccessResponse(ticketDto);
        }

        public async Task<ServiceResponse<TicketDto?>> CreateAsync(CreateTicketDto createTicketDto)
        {
            var ticket = _ticketMapper.MapToTicket(createTicketDto);
            var newTicketRecord = await _ticketRepositoy.AddAsync(ticket);

            var ticketDto = _ticketMapper.MapToTicketDto(newTicketRecord);
            return ServiceResponse<TicketDto?>.SuccessResponse(ticketDto);
        }
    }
}
