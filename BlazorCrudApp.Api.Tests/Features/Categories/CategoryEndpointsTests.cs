using System.Net;
using System.Net.Http.Json;
using BlazorCrudApp.Api.Features.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorCrudApp.Api.Tests.Features.Categories;

public class CategoryEndpointsTests(ApiFactory factory)
    : IClassFixture<ApiFactory>, IAsyncLifetime
{
    private readonly HttpClient _client = factory.CreateClient();

    public async Task InitializeAsync()
    {
        await factory.ResetDatabaseAsync();
    }
    
    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task GetCategories_ReturnsOk()
    {
        // Act
        var response =  await _client.GetAsync("/api/categories");
        var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<Category>>();
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, result?.Count);
    }
    [Fact]
    public async Task GetCategory_WhenExists_ReturnsOk()
    {
        // Arrange
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        
        // Act
        var response = await _client.GetAsync($"/api/categories/{id}");
        var result = await response.Content.ReadFromJsonAsync<Category>();
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal("Beverages", result.Name);
    }
    [Fact]
    public async Task GetCategory_WhenNotExists_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        var response = await _client.GetAsync($"/api/categories/{id}");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task CreateCategory_WithValidRequest_ReturnsCreated()
    {
        // Arrange
        var request = new CreateCategoryRequest
        {
            Name = "Grocery"
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/categories", request);
        var category = await response.Content.ReadFromJsonAsync<Category>();
        
        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(category);
        Assert.Equal("Grocery", category.Name);
        Assert.Equal(
            $"/api/categories/{category.Id}",
            response.Headers.Location?.ToString());
    }
    [Fact]
    public async Task CreateCategory_WithEmptyName_ReturnsValidationProblems()
    {
        // Arrange
        var request = new CreateCategoryRequest
        {
            Name = ""
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/categories", request);
        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(problem);
        Assert.Contains("Name", problem.Errors.Keys);
    }
    [Fact]
    public async Task CreateCategory_WhenNameAlreadyExists_ReturnsConflict()
    {
        // Arrange
        var request = new CreateCategoryRequest
        {
            Name = "Beverages"
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/api/categories", request);
        
        // Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
    
    [Fact]
    public async Task UpdateCategory_WithValidRequest_ReturnsNoContent()
    {
        // Arrange
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var request = new UpdateCategoryRequest
        {
            Name = "School Supplies"
        };
        
        // Act
        var response = await _client.PutAsJsonAsync($"/api/categories/{id}", request);
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    [Fact]
    public async Task UpdateCategory_WhenNotExists_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new UpdateCategoryRequest
        {
            Name = "School Supplies"
        };
        
        // Act
        var response = await _client.PutAsJsonAsync($"/api/categories/{id}", request);
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task UpdateCategory_WithEmptyName_ReturnsValidationProblems()
    {
        // Arrange
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var request = new UpdateCategoryRequest
        {
            Name = ""
        };
        
        // Act
        var response = await _client.PutAsJsonAsync($"/api/categories/{id}", request);
        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        
        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(problem);
        Assert.Contains("Name",  problem.Errors.Keys);
    }

    [Fact]
    public async Task UpdateCategory_WhenNameAlreadyExists_ReturnsConflict()
    {
        // Arrange
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var request = new UpdateCategoryRequest
        {
            Name = "Food"
        };
        
        // Act
        var response = await _client.PutAsJsonAsync($"/api/categories/{id}", request);
        
        // Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task DeleteCategory_WhenExists_ReturnsNoContent()
    {
        // Arrange
        var category = await CreateCategoryAsync("Grocery");
        
        // Act
        var response = await _client.DeleteAsync($"/api/categories/{category.Id}");
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Fact]
    public async Task DeleteCategory_WhenNotFound_ReturnsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        var response = await _client.DeleteAsync($"/api/categories/{id}");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    private async Task<Category> CreateCategoryAsync(string name)
    {
        await using var scope = factory.Services.CreateAsyncScope();

        var db = scope.ServiceProvider
            .GetRequiredService<AppDbContext>();
        
        var category = new Category(name);
        
        db.Categories.Add(category);
        
        await db.SaveChangesAsync();

        return category;
    }
}