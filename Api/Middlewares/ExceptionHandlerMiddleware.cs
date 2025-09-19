using Api.ViewResponse;
using Domain.Exceptions;

namespace Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case EntityNotFoundException e:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return context.Response.WriteAsJsonAsync(ApiResponse.NotFound(e.Message));

            case BadRequestException e:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return context.Response.WriteAsJsonAsync(ApiResponse.BadRequest(e.Message));

            case InternalServerErrorException e:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return context.Response.WriteAsJsonAsync(ApiResponse.ServerError(e.Message));

            case UnauthorizedException e:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return context.Response.WriteAsJsonAsync(ApiResponse.Unauthorized(e.Message));

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return context.Response.WriteAsJsonAsync(ApiResponse.ServerError("An unexpected error occurred."));
        }
    }
}
