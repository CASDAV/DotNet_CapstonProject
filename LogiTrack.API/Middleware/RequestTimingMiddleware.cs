using System.Diagnostics;

namespace LogiTrack.API.Middleware;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();

        await _next(context);

        stopwatch.Stop();

        var elapsed = stopwatch.ElapsedMilliseconds;

        _logger.LogInformation($"Request {context.Request.Method} {context.Request.Path} completed in {elapsed} ms");
    }
}

public static class RequestTimingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestTiming(this IApplicationBuilder app) => app.UseMiddleware<RequestTimingMiddleware>();
}
