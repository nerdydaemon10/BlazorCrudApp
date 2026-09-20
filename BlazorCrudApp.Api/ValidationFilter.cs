using FluentValidation;

namespace BlazorCrudApp.Api;

public sealed class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices
            .GetService<IValidator<T>>();

        if (validator is null)
            return await next(context);

        var argument = context.Arguments
            .OfType<T>()
            .FirstOrDefault();
        
        if (argument is null)
            return await next(context);

        var result = await validator.ValidateAsync(argument);
        
        if (result.IsValid) 
            return await next(context);
        
        var errors = result.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                x => x.Key,
                x => x.Select(e => e.ErrorMessage).ToArray());
        
        return Results.ValidationProblem(errors);
    }
}