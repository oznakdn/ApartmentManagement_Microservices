using Account.Domain.QueryEntities;
using Microsoft.EntityFrameworkCore;

namespace Account.Infrastructure.Contexts;

public class QueryDbContext : DbContext
{
    public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options)
    {

    }

    public DbSet<UserQuery> Users { get; set; }
}
