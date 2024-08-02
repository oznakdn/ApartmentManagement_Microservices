using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Shared.Caching;

public static class DependencyInjection
{
    public static void AddCaching(this IServiceCollection services, Action<RedisOption> option)
    {

        var redisOption = new RedisOption();
        option.Invoke(redisOption);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisOption.Host;
            options.ConfigurationOptions = new ConfigurationOptions
            {
                AbortOnConnectFail = true,
                ClientName = redisOption.ClientName,
                Password = redisOption.Password,
                EndPoints = { options.Configuration! }
            };
        });

        services.AddScoped<IDistributedCacheService, DistributedCacheService>();

    }
}
