using System.Collections.Concurrent;

namespace BlazorCrudApp.Client.Features.Products;

public class ProductRepository(ProductApiClient client)
{
    private readonly ConcurrentDictionary<string, Task<IEnumerable<Product>>> _cache = new();
    
    public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        var key = "";
        return await _cache.GetOrAdd(key, _ => client.GetProductsAsync(cancellationToken));
    }

    public void Invalidate()
    {
        _cache.Clear();
    }
}