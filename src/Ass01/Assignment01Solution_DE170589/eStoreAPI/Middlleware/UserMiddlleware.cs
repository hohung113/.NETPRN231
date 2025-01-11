namespace eStoreAPI.Middlleware
{
    public class UserMiddlleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserMiddlleware> _logger;

        public UserMiddlleware(RequestDelegate next, ILogger<UserMiddlleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation("Processing request: " + context.Request.Path);

            await _next(context);

            _logger.LogInformation("Finished processing request: " + context.Request.Path);
        }
    }
}
