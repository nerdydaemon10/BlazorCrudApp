namespace BlazorCrudApp.Client.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(
        this IServiceCollection services, 
        string baseAddress)
    {
        services.AddTransient<ServerUnavailableHandler>();
        
        services.AddHttpClient(ApiClientNames.BlazorCrudApp,
            c => c.BaseAddress = new Uri(baseAddress))
            .AddHttpMessageHandler<ServerUnavailableHandler>();
        
        return services;
    }
}