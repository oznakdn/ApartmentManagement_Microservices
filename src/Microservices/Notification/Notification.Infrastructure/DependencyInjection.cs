using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Notification.Infrastructure.Contexts;

namespace Notification.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoOption>(configuration.GetSection(nameof(MongoOption)));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOption>>().Value);
        services.AddScoped(typeof(MongoDbContext<>));
    }
}
