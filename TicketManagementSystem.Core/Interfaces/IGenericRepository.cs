namespace TicketManagementSystem.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAsIQueryable();

        Task<T?> GetByIdAsync(int id);

        Task AddAsync(T entity);
    }
}
