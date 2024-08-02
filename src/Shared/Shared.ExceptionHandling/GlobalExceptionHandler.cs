using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Shared.ExceptionHandling;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string title = "Error";
        string message = "Internal server error occurred.";
        int statusCode = (int)HttpStatusCode.InternalServerError;

        try
        {
            await next(context);

            if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
            {
                title = "Warning";
                message = "Too many requests. Please try again later.";
                statusCode = (int)HttpStatusCode.TooManyRequests;
                await ModifyHeader(context, title, message, statusCode);
            }

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                title = "Alert";
                message = "Unauthorized. Please login first.";
                statusCode = (int)HttpStatusCode.Unauthorized;
                await ModifyHeader(context, title, message, statusCode);
            }

            if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                title = "Out of Access";
                message = "Forbidden. You don't have permission to access this resource.";
                statusCode = (int)HttpStatusCode.Forbidden;
                await ModifyHeader(context, title, message, statusCode);
            }

        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);

            if (ex is TaskCanceledException || ex is TimeoutException)
            {
                title = "Out of time";
                message = "Request timed out. Please try again later.";
                statusCode = (int)HttpStatusCode.RequestTimeout;
            }
            await ModifyHeader(context, title, message, statusCode);
        }
    }

    private static async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
        {
            Detail = message,
            Status = statusCode,
            Title = title
        }), CancellationToken.None);

        return;
    }


}
