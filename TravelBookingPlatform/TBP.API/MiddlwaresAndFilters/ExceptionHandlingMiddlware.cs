using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Application.Exceptions;
namespace TravelBookingPlatform.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (EntityNotFoundException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
        }
        catch (InvalidCredentialsException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);
        }
        catch (DuplicateUsernameException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (DuplicateEmailException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (RoomNotAvailableException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (RoomAlreadyFeaturedException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.Conflict);
        }
        catch (InvalidOperationException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (DbUpdateException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        _logger.LogError($"An exception of type {exception.GetType().Name} was caught!: {exception.Message}");
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var result = JsonSerializer.Serialize(new { error = exception.Message });
        return context.Response.WriteAsync(result);
    }
}