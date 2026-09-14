using System.Collections.Concurrent;

namespace BlazorCrudApp.Client.Features.Categories;

public class CategoryRepository(CategoryApiClient client)
{
    private readonly ConcurrentDictionary<string, Task<IEnumerable<Category>>> _cache = new();
    
    public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var key = "";
        return await _cache.GetOrAdd(key, _ => client.GetCategoriesAsync(cancellationToken));
    }
    public void Invalidate()
    {
        _cache.Clear();
    }
}