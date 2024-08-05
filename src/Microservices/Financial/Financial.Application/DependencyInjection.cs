using Microsoft.Extensions.DependencyInjection;
using Financial.Infrastructure;
using FluentValidation;
using MediatR;
using System.Reflection;
namespace Financial.Application;

public static class DependencyInjection
{
    public static void AddApplicationService(this IServiceCollection services, string commandConnectionString, string queryConnectionString)
    {
        services.AddInfrastructureService(commandConnectionString, queryConnectionString);
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
