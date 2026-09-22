using System.Net;
using System.Net.Http.Json;

namespace BlazorCrudApp.Client.Infrastructure;

public class ApiErrorHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response;
        
        try
        {
            response = await base.SendAsync(request, cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            throw new ServerUnavailableException("The server is currently unavailable. Please try again.", ex);
        }
        
        if (response.IsSuccessStatusCode)
            return response;
        
        switch (response.StatusCode)
        {
            case HttpStatusCode.BadRequest:
                await ThrowBadRequestAsync(response);
                break;
            case HttpStatusCode.Conflict:
                await ThrowConflictAsync(response);
                break;
        }
        
        return response;
    }
    
    private static async Task ThrowBadRequestAsync(HttpResponseMessage response)
    {
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        
        throw new BadRequestException(
            problemDetails?.Title ?? "The request was invalid.", 
            problemDetails?.Errors ?? []);
    }
    private static async Task ThrowConflictAsync(HttpResponseMessage response)
    {
        var problemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        
        throw new ConflictException(
            problemDetails?.Title ?? "The request was invalid.", 
            problemDetails?.Errors ?? []);
    }
}