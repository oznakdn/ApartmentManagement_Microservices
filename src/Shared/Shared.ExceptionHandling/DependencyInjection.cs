using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.ExceptionHandling;

public static class DependencyInjection
{
    public static void AddExceptonHandlerService(this IServiceCollection services)
    {
        services.AddScoped<GlobalExceptionHandler>();
    }

    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();
    }
}
