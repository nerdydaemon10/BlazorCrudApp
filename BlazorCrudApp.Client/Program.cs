using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorCrudApp.Client;
using BlazorCrudApp.Client.Features;
using BlazorCrudApp.Client.Infrastructure;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("Api"));

var apiSettings = builder.Configuration
    .GetSection("Api")
    .Get<ApiSettings>();

builder.Services.AddInfrastructureModule(apiSettings?.BaseUrl ?? builder.HostEnvironment.BaseAddress);
builder.Services.AddFeaturesModule();
builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();