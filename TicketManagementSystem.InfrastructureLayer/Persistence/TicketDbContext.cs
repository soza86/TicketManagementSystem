using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.Core.Entities;

namespace TicketManagementSystem.InfrastructureLayer.Persistence
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Ticket>()
                .HasIndex(o => o.Number)
                .IsUnique();
        }
    }
}
