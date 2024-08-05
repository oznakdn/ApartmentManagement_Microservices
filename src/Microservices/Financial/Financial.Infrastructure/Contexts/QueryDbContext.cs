using Financial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Financial.Infrastructure.Contexts;

public class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options)
    {
        
    }

    public DbSet<Expence> Expences { get; set; }
    public DbSet<ExpenceItem> ExpenceItems { get; set; }
    public DbSet<Payment> Payments { get; set; }

}
