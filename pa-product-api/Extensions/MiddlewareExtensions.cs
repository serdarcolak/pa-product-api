using pa_product_api.Middlewares;

namespace pa_product_api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}