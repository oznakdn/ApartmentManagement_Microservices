using Account.Domain.Entities;
using Account.Infrastructure.Contexts;
using Account.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureService(this IServiceCollection services, string commandConnectionString, string queryConnectionString)
    {
        services.AddDbContext<CommandDbContext>(opt => opt.UseSqlServer(commandConnectionString));
        services.AddDbContext<QueryDbContext>(opt => opt.UseSqlServer(queryConnectionString));


        services.AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CommandDbContext>();

    }
}
