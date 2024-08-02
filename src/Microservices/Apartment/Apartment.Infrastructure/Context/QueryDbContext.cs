using Apartment.Domain.QueryEntities;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Infrastructure.Context;

public class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<SiteQuery> Sites { get; set; }
    public DbSet<BlockQuery> Blocks { get; set; }
    public DbSet<UnitQuery> Units { get; set; }
    public DbSet<VisitQuery> Visits { get; set; }

   
}
