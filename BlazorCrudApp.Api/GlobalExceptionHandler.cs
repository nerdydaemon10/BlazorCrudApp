using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCrudApp.Api;

public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        // logger.LogError(
        //     exception,
        //     "Unhandled exception occured.");
        
        var problem = exception switch
        {
            ConflictException conflict => CreateConflictProblem(conflict),
            _ => null
        };
        
        if (problem is null)
            return false;
        
        httpContext.Response.StatusCode = problem.Status!.Value;
        
        return await problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problem
            });
    }
    
    private static ProblemDetails CreateConflictProblem(
        ConflictException exception)
    {
        var problem = new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = "Conflict",
            Detail = exception.Message,
        };
        
        if (exception.PropertyName is not null)
        {
            problem.Extensions["errors"] =
                new Dictionary<string, string[]>
                {
                    [exception.PropertyName] = [exception.Message]
                };
        }
        
        return problem;
    }
}