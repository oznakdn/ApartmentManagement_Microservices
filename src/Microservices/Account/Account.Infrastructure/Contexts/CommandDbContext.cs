using Account.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Constants;

namespace Account.Infrastructure.Contexts;

public class CommandDbContext : IdentityDbContext<User>
{
    public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Id = Ulid.NewUlid().ToString(),
                Name = RoleConstant.ADMIN,
                NormalizedName = nameof(RoleConstant.ADMIN)
            },
            new IdentityRole
            {
                Id = Ulid.NewUlid().ToString(),
                Name = RoleConstant.MANAGER,
                NormalizedName = nameof(RoleConstant.MANAGER)
            },
            new IdentityRole
            {
                Id = Ulid.NewUlid().ToString(),
                Name = RoleConstant.RESIDENT,
                NormalizedName = nameof(RoleConstant.RESIDENT)
            },
            new IdentityRole
            {
                Id = Ulid.NewUlid().ToString(),
                Name = RoleConstant.GUARD,
                NormalizedName = nameof(RoleConstant.GUARD)
            });

    }
}
