using blog.Exceptions;
using System.Text.Json;

namespace blog.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int status = StatusCodes.Status500InternalServerError;
            string message = "An unexpected error occurred.";
            object? errors = null;

            if (exception is ApiException apiEx)
            {
                status = apiEx.StatusCode;
                message = apiEx.Message;
                errors = apiEx.Errors;
            }

            var payload = new
            {
                statusCode = status,
                message,
                errors,
                detail = _env.EnvironmentName == "Development" ? exception.StackTrace : null
            };

            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;
            return context.Response.WriteAsync(json);
        }
    }
}
