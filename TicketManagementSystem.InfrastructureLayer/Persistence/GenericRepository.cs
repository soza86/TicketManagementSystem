using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.Core.Interfaces;

namespace TicketManagementSystem.InfrastructureLayer.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TicketDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(TicketDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<List<T?>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
