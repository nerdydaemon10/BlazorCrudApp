using BlazorCrudApp.Client.Features.Categories;
using BlazorCrudApp.Client.Features.Products;

namespace BlazorCrudApp.Client.Features;

public static class FeaturesModule
{
    public static IServiceCollection AddFeaturesModule(this IServiceCollection services)
    {
        services.AddCategoriesModule();
        services.AddProductsModule();
        return services;
    }
}