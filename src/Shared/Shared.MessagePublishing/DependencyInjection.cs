using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shared.MessagePublising;

namespace Shared.MessagePublishing;

public static class DependencyInjection
{
    public static void AddMessagePublisherService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOption>(configuration.GetSection(nameof(RabbitMqOption)));
        services.AddSingleton(sp => sp.GetRequiredService<IOptions<RabbitMqOption>>().Value);
        services.AddScoped<IMessagePublisher, MessagePublisher>();
    }
}
