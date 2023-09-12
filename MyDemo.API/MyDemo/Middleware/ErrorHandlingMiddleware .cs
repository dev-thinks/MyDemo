namespace MyDemo.Core.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log error
            if (context.Response.StatusCode == 404)
            {
                // Redirect to 404 page
                context.Response.Redirect("/error/404");
            }
            else
            {
                // Handle other errors
                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("An unexpected error occurred!");
            }
        }
    }
}

public static class ErrorHandlingMiddlewareMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddlewareMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}