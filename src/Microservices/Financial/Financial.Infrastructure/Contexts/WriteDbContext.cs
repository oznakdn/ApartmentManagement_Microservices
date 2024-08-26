using Financial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financial.Infrastructure.Contexts;

public class WriteDbContext : DbContext
{
    public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
    {
        
    }

    public DbSet<Expence> Expences  { get; set; }
    public DbSet<ExpenceItem> ExpenceItems  { get; set; }
    public DbSet<Payment> Payments  { get; set; }

    
}
