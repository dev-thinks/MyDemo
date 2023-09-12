
namespace MyDemo.Core.Middleware;

public class RequestLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLogMiddleware> _logger;

    public RequestLogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<RequestLogMiddleware> logger)
    {
        logger.LogDebug("Request received: {Method} {Path}", context.Request.Method, context.Request.Path);

        await _next(context);

        logger.LogDebug("Response sent: {StatusCode}", context.Response.StatusCode);
    }
}

public static class RequestLogMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLogMiddleware>();
    }
}
