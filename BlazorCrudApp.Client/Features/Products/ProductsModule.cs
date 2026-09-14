using BlazorCrudApp.Client.Infrastructure;

namespace BlazorCrudApp.Client.Features.Products;

public static class ProductsModule
{
    public static IServiceCollection AddProductsModule(this IServiceCollection services)
    {
        services.AddHttpClient<ProductApiClient>(ApiClientNames.BlazorCrudApp);
        services.AddScoped<ProductRepository>();
        return services;
    }
}