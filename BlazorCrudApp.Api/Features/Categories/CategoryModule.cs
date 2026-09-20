using FluentValidation;

namespace BlazorCrudApp.Api.Features.Categories;

public static class CategoryModule
{
    public static IServiceCollection AddCategoryModule(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateCategoryValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateCategoryValidator>();
        services.AddScoped<CategoryService>();
        return services;
    }
}