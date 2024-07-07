namespace pa_product_api.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<LoggingMiddleware> logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        logger.LogInformation("Action Run....");
        await next(context);
        logger.LogInformation("Action finish....");
    }
}