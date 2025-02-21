using TicketManagementSystem.Core.Interfaces;

namespace TicketManagementSystem.InfrastructureLayer.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public Task<List<T?>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
