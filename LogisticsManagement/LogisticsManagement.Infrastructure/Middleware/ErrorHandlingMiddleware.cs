using LogisticsManagement.Domain.Exceptions;

namespace LogisticsManagement.Infrastructure.Middleware;

using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlConnector;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = StatusCodes.Status500InternalServerError;
        var result = string.Empty;

        switch (exception)
        {
            case DbUpdateException { InnerException: MySqlException { Number: 1062 } }:
                code = StatusCodes.Status400BadRequest;
                result = "Supplier email already exists";
                break;
            case MySqlException:
                code = StatusCodes.Status503ServiceUnavailable;
                result = "Unable to connect to the database. Please try again later.";
                break;
            case HttpException httpException:
                code = httpException.StatusCode;
                result = httpException.Message;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;
        var response = new { StatusCode = code, Message = result };
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}