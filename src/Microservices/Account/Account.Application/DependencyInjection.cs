using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Account.Infrastructure;
using Microsoft.Extensions.Options;
using MediatR;
using FluentValidation;
using Account.Application.Helpers.Token;

namespace Account.Application;

public static class DependencyInjection
{
    public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration, string commandConnectionString, string queryConnectionString)
    {
        services.AddInfrastructureService(commandConnectionString, queryConnectionString);

        services.Configure<JwtOption>(configuration.GetSection(nameof(JwtOption)));

        services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtOption>>().Value);

        services.AddScoped<JwtTokenHelper>();

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    }
}
