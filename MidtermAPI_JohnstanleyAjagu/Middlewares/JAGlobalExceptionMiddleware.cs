namespace MidtermAPI_JohnstanleyAjagu.Middlewares
{
    public sealed class JAGlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JAGlobalExceptionMiddleware> _logger;
        public JAGlobalExceptionMiddleware(RequestDelegate next, ILogger<JAGlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception for {Method} {Path}",
                context.Request?.Method, context.Request?.Path);
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "ServerError",
                        message = "An unexpected error occurred."
                    });
                }
            }
        }
    }
    
}
