using System.Text.Json;
using Microsoft.AspNetCore.Http;
using SupplierManagement.Domain.Exceptions;

namespace SupplierManagement.Infrastructure.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (HttpException ex)
        {
            httpContext.Response.StatusCode = ex.StatusCode;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                code = ex.StatusCode,
                message = ex.Message
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            await httpContext.Response.WriteAsync(jsonResponse);
        }
    }
}