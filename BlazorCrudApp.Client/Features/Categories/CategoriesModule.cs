using BlazorCrudApp.Client.Infrastructure;

namespace BlazorCrudApp.Client.Features.Categories;

public static class CategoriesModule
{
    public static IServiceCollection AddCategoriesModule(this IServiceCollection services)
    {
        services.AddHttpClient<CategoryApiClient>(ApiClientNames.BlazorCrudApp);
        services.AddScoped<CategoryRepository>();
        return services;
    }
}