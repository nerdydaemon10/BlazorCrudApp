using System.Net.Http.Json;

namespace BlazorCrudApp.Client.Features.Products;

public class ProductApiClient(HttpClient http)
{
    public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var response = await http.GetAsync("products", cancellationToken);
        return await response.Content
            .ReadFromJsonAsync<IEnumerable<Product>>(cancellationToken) ?? [];
    }
}