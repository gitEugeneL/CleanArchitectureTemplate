using Domain.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Utils;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken ct)
    {
        var (status, message) = exception switch
        {
            NotFoundException e => (StatusCodes.Status404NotFound, e.Message),
            ConflictException e => (StatusCodes.Status409Conflict, e.Message),
            BadHttpRequestException _ => (StatusCodes.Status400BadRequest, "Invalid request format"),
            _ => (StatusCodes.Status500InternalServerError, "Something went wrong")
        };
        
        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(new { error = message }, ct);
        return true;
    }
}