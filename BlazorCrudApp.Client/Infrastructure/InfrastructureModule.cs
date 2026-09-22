namespace BlazorCrudApp.Client.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(
        this IServiceCollection services, 
        string baseAddress)
    {
        services.AddTransient<ApiErrorHandler>();
        
        services.AddHttpClient(ApiClientNames.BlazorCrudApp,
            c => c.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<ApiErrorHandler>();
        
        return services;
    }
}