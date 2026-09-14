using Microsoft.AspNetCore.Mvc.Testing;

namespace BlazorCrudApp.Api.Tests.Features.Categories;

public class CategoryEndpointsTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();
    
    [Fact]
    public async Task GetCategoriesAsync_ReturnsOk()
    {
        var response =  await _client.GetAsync("/api/categories");
    }
}