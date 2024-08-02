using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Shared.Logging;

public static class DependencyInjection
{
    public static void AddSerilogService(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
       .MinimumLevel.Information()
       .WriteTo.Console()
       .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
       .CreateLogger();
    }

    public static void UseSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog();
    }
}
