using DataEntities;
using Microsoft.EntityFrameworkCore;
namespace DataCore
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Ticket> Tickets => Set<Ticket>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
 