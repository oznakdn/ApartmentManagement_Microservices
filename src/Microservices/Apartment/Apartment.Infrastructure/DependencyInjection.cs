using Apartment.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Apartment.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureService(this IServiceCollection services, string commandConnectionString, string queryConnectionString)
    {
        services.AddDbContext<CommandDbContext>(opt => opt.UseNpgsql(commandConnectionString));
        services.AddDbContext<QueryDbContext>(opt => opt.UseNpgsql(queryConnectionString));
    }
}
