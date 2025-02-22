using System.Linq.Expressions;

namespace TicketManagementSystem.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<T?> GetByFieldAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task SaveChangesAsync();
    }
}
