namespace eStoreAPI.Middlleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
                _logger.LogError(ex, "An exception was thrown but handled gracefully.");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var result = new { message = "Something went wrong." };
                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}