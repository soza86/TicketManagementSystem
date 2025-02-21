namespace TicketManagementSystem.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T?>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task AddAsync(T entity);
    }
}
