using Apartment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Infrastructure.Context;

public class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options)
    {
        
    }

    public DbSet<Site> Sites { get; set; }
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Visit> Visits { get; set; }


}
