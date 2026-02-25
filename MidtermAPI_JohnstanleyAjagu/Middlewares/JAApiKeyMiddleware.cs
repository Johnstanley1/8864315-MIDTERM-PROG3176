namespace MidtermAPI_JohnstanleyAjagu.Middlewares
{
    public class JAApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _apiKey;
        public JAApiKeyMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _apiKey = config.GetValue<string>("ApiKey");
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-API-Key", out var providedKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key was not provided.");
                return;
            }
            if (_apiKey != providedKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid or missing API key.");
                return;
            }
            await _next(context);
        }
    }
}