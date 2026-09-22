using System.Net.Http.Json;

namespace BlazorCrudApp.Client.Features.Categories;

public class CategoryApiClient(HttpClient http)
{
    public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        var response = await http.GetAsync("categories", cancellationToken);
        return await response.Content.ReadFromJsonAsync<IEnumerable<Category>>(cancellationToken) ?? [];
    }
    public async Task<Category> CreateCategoryAsync(
        Category category, 
        CancellationToken cancellationToken = default)
    {
        using var response = await http.PostAsJsonAsync("categories", category, cancellationToken);
        return await response.Content.ReadFromJsonAsync<Category>(cancellationToken) 
               ?? throw new InvalidOperationException( "The API returned an empty response.");
    }
}