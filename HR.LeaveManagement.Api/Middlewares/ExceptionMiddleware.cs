using System.Net;
using HR.LeaveManagement.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        ValidationProblemDetails problemDetails;

        switch (exception) 
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problemDetails = new ValidationProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                problemDetails = new ValidationProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Detail = notFoundException.InnerException?.Message,
                    Type = nameof(NotFoundException)
                };
                break;
            default:
                problemDetails = new ValidationProblemDetails
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = exception.StackTrace,
                };
                break;
        }
        
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}