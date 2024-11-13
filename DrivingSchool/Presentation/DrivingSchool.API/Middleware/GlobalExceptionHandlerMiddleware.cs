using DrivingSchool.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace DrivingSchool.API.Middleware;

public class GlobalExceptionHandlerMiddleware(RequestDelegate _next, ILogger<GlobalExceptionHandlerMiddleware> _logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (ex is not HttpException httpException || httpException.InnerException != null)
            {
                _logger?.LogError(ex.ToString());
            }
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        string result;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        if (ex is not HttpException httpException)
        {
            result = JsonSerializer.Serialize(new { error = ex.Message });
            return context.Response.WriteAsync(result);
        }

        context.Response.StatusCode = (int)httpException.StatusCode;
#if DEBUG
        result = JsonSerializer.Serialize(new { detail = ex.Message, data = httpException.ObjectData });
#else
        result = JsonSerializer.Serialize(new { detail = ex.Message });
#endif
        return context.Response.WriteAsync(result);
    }
}