using Apartment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Apartment.Infrastructure.Context;

public class CommandDbContext : DbContext
{
    public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options)
    {
        
    }

    public DbSet<Site>Sites { get; set; }
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Visit> Visits { get; set; }


}
