using System.Net;

namespace BlazorCrudApp.Api.Tests.Features.Categories;

public class CategoryEndpointsTests(ApiFactory factory)
    : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();
    
    [Fact]
    public async Task GetCategoriesAsync_ReturnsOk()
    {
        // Act
        var response =  await _client.GetAsync("/api/categories");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}