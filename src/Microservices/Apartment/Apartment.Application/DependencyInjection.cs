using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Apartment.Infrastructure;
using MediatR;
using System.Reflection;
using Shared.MessagePublishing;

namespace Apartment.Application;

public static class DependencyInjection
{
    public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration, string commandConnectionString, string queryConnectionString)
    {
        services.AddInfrastructureService(commandConnectionString, queryConnectionString);
        services.AddMessagePublisherService(configuration);
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
