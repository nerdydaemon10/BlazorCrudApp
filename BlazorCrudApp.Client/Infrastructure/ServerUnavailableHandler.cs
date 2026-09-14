using System.Text.Json;

namespace BlazorCrudApp.Client.Infrastructure;

public class ServerUnavailableHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        { 
            return await base.SendAsync(request, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            throw new ServerUnavailableException("The server is currently unavailable. Please try again.", ex);
        }
    }
}