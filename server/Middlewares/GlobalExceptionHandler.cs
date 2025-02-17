using server.Application.Common.Exceptions.Auth;
using server.Domain.Exceptions;

namespace server.Middlewares;

public class GlobalExceptionHandler(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

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
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        switch (exception)
        {
            case NotFoundException nFEx:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(nFEx.Message);
                break;
            case IdentityException iEx:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(iEx.Message);
                break;
            case AlreadyExistsException aEx:
                context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                await context.Response.WriteAsJsonAsync(aEx.Message);
                break;
            default:
                await context.Response.WriteAsJsonAsync("Server error");
                break;
        }
    }
}
