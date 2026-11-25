using System.Diagnostics;

namespace VisaService.WebHost.Middlewares;

/// <summary>Пишет в лог метод, путь, код ответа и время выполнения каждого запроса.</summary>
public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        await next(context);
        stopwatch.Stop();

        logger.LogInformation("{Method} {Path} -> {StatusCode} ({Elapsed} ms)",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}
