using System.Net;
using System.Text.Json;
using worker.platform.application.Common.Exceptions;

namespace worker.platform.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An error occurred");

        var response = context.Response;
        response.ContentType = "application/json";

        response.StatusCode = exception switch
        {
            NotFoundException _ => (int) HttpStatusCode.NotFound,
            UnauthorizedException _ => (int) HttpStatusCode.Unauthorized,
            ArgumentException _ => (int) HttpStatusCode.BadRequest,
            _ => (int) HttpStatusCode.InternalServerError
        };

        var result = JsonSerializer.Serialize(new { message = exception.Message });
        await response.WriteAsync(result);
    }
}
