using Financial.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Financial.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructureService(this IServiceCollection services, string commandConnectionString, string queryConnectionString)
    {
        services.AddDbContext<CommandDbContext>(opt=> opt.UseMySql(commandConnectionString,ServerVersion.Create(Version.Parse("8.0.0"),ServerType.MySql)));
        services.AddDbContext<QueryDbContext>(opt => opt.UseMySql(queryConnectionString, ServerVersion.Create(Version.Parse("8.0.0"), ServerType.MySql)));
    }
}
