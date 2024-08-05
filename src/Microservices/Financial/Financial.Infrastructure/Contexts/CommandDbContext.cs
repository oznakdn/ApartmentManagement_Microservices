using Financial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financial.Infrastructure.Contexts;

public class CommandDbContext : DbContext
{
    public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options)
    {
        
    }

    public DbSet<Expence> Expences  { get; set; }
    public DbSet<ExpenceItem> ExpenceItems  { get; set; }
    public DbSet<Payment> Payments  { get; set; }

    
}
