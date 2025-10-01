using NetUsersApi.Models;
using NetUsersApi.Exceptions;
using System.Net;
using System.Text.Json;

namespace NetUsersApi.Middleware;

/// <summary>
/// Middleware to handle exceptions globally and return consistent error responses
/// </summary>
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex, "An unhandled exception occurred while processing the request");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, errorCode, details) = MapExceptionToStatusCode(exception);

        var apiError = new ApiError
        {
            ErrorCode = errorCode,
            Message = exception.Message,
            Details = details ?? (context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment() 
                ? exception.StackTrace 
                : null),
            Timestamp = DateTime.UtcNow,
            Path = context.Request.Path.Value ?? string.Empty
        };

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(apiError, options);
        await context.Response.WriteAsync(json);
    }

    private static (HttpStatusCode statusCode, string errorCode, object? details) MapExceptionToStatusCode(Exception exception)
    {
        return exception switch
        {
            ResourceNotFoundException resourceNotFound => (
                HttpStatusCode.NotFound, 
                "RESOURCE_NOT_FOUND", 
                new { resourceType = resourceNotFound.ResourceType, resourceId = resourceNotFound.ResourceId }
            ),
            ValidationException validation => (
                HttpStatusCode.BadRequest, 
                "VALIDATION_ERROR", 
                validation.ValidationErrors.Any() ? validation.ValidationErrors : null
            ),
            BusinessRuleException businessRule => (
                HttpStatusCode.BadRequest, 
                businessRule.RuleCode, 
                null
            ),
            ArgumentNullException => (HttpStatusCode.BadRequest, "MISSING_ARGUMENT", null),
            ArgumentException => (HttpStatusCode.BadRequest, "INVALID_ARGUMENT", null),
            InvalidOperationException => (HttpStatusCode.BadRequest, "INVALID_OPERATION", null),
            NotImplementedException => (HttpStatusCode.NotImplemented, "NOT_IMPLEMENTED", null),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "UNAUTHORIZED", null),
            TimeoutException => (HttpStatusCode.RequestTimeout, "TIMEOUT", null),
            _ => (HttpStatusCode.InternalServerError, "INTERNAL_ERROR", null)
        };
    }
}