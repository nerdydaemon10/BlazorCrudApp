using BlazorCrudApp.Client.Infrastructure;
using FluentValidation;

namespace BlazorCrudApp.Client.Features.Categories;

public static class CategoryModule
{
    public static IServiceCollection AddCategoriesModule(this IServiceCollection services)
    {
        services.AddHttpClient<CategoryApiClient>(ApiClientNames.BlazorCrudApp);
        services.AddScoped<CategoryRepository>();
        
        services.AddSingleton<IValidator<Category>, CategoryValidator>();
        
        return services;
    }
}