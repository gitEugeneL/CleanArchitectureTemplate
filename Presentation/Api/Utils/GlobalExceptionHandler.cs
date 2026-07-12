using Domain.Exceptions.Common;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Utils;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken ct)
    {
        var (status, body) = exception switch
        {
            ValidationException ve => (StatusCodes.Status400BadRequest, (object)new
            {
               errors = ve.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage }) 
            }),
            NotFoundException e => (StatusCodes.Status404NotFound, new {error = e.Message}),
            ConflictException e => (StatusCodes.Status409Conflict, new {error = e.Message}),
            BadHttpRequestException _ => (StatusCodes.Status400BadRequest, new {error = "Invalid request format"}),
            _ => (StatusCodes.Status500InternalServerError, new {error = "Something went wrong"})
        };
        
        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(body, ct);
        return true;
    }
}