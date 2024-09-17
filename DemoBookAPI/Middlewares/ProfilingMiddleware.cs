using System.Diagnostics;

namespace DemoBookAPI.Middlewares
{
    public class ProfilingMiddleware
    {
        /* To make your class a middleware:
        1- Pass RequestDelegate in the constructor
        2- Create method name Invoke(HttpContext context)
        3- Register your class as middleware app.UseMiddleware<ProfilingMiddleware>();
        */
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleware> _logger;
        public ProfilingMiddleware(RequestDelegate next, ILogger<ProfilingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();
            _logger.LogInformation($"Request `{context.Request.Path}` took `{stopwatch.ElapsedMilliseconds}`ms");
        }

    }
}
