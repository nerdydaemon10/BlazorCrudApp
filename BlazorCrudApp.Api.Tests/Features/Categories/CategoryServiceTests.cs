using BlazorCrudApp.Api.Features.Categories;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrudApp.Api.Tests.Features.Categories;

public class CategoryServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsCategories()
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        var service = new CategoryService(db);
        
        // Act
        var result = await service.GetAllAsync();
        
        // Assert
        Assert.Equal(2, result.Count);
    }
    
    // [Fact]
    // public async Task GetAllAsync_WhenEmpty_ReturnsEmptyList()
    // {}
    
    [Fact]
    public async Task GetAllAsync_ReturnsCategoriesOrderedByName()
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        var service = new CategoryService(db);
        
        // Act
        var result = await service.GetAllAsync();
        
        // Assert
        Assert.Equal(
            ["Beverages", "Food"],
            result.Select(x => x.Name));
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnsCategory()
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        var service = new CategoryService(db);
        
        // Act
        var result = await service.GetByIdAsync(Guid.Parse("11111111-1111-1111-1111-111111111111"));
        
        // Assert
        Assert.NotNull(result);
    }
    [Fact]
    public async Task GetByIdAsync_WhenCategoryDoesNotExist_ReturnsNull()
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        var service = new CategoryService(db);
        
        // Act
        var result = await service.GetByIdAsync(Guid.NewGuid());
        
        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public async Task CreateAsync_WithValidRequest_CreatesCategory()
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        
        var service = new CategoryService(db);
        var request = new CreateCategoryRequest
        {
            Name = "Grocery"
        };
        
        // Act
        var result = await service.CreateAsync(request);
        
        // Assert
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(request.Name, result.Name);
    }
    
    [Fact]
    public async Task CreateAsync_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        
        var service = new CategoryService(db);
        var request = new CreateCategoryRequest
        {
            Name = string.Empty
        };
        
        // Act
        var result = await Assert.ThrowsAsync<ArgumentException>(
            () => service.CreateAsync(request));
        
        // Assert
        Assert.NotNull(result);
    }
    [Theory]
    [InlineData("Beverages")]
    [InlineData("BEVERAGES")]
    [InlineData("beverages")]
    public async Task CreateAsync_WhenNameAlreadyExists_ThrowsConflictException(string name)
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        
        var service = new CategoryService(db);
        var request = new CreateCategoryRequest
        {
            Name = name
        };
        
        // Act
        var exception = await Assert.ThrowsAsync<ConflictException>(
            () => service.CreateAsync(request));
        
        // Assert
        Assert.NotNull(exception);
    }
    [Fact]
    public async Task UpdateAsync_WithValidRequest_UpdatesCategory()
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        
        var service = new CategoryService(db);
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var request = new UpdateCategoryRequest
        {
            Name = "Grocery"
        };
        
        // Act
        var result = await service.UpdateAsync(id, request);
        
        // Assert
        Assert.True(result);
        
        var category = await db.Categories
            .FirstOrDefaultAsync(x => x.Id == id);
        
        Assert.Equal("Grocery", category?.Name);
    }
    [Theory]
    [InlineData("Beverages")]
    [InlineData("BEVERAGES")]
    [InlineData("beverages")]
    public async Task UpdateAsync_WhenNameExists_ThrowsConflictException(string name)
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        
        var service = new CategoryService(db);
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var request = new UpdateCategoryRequest
        {
            Name = name
        };
        
        // Act
        var result = await Assert.ThrowsAsync<ConflictException>(
            () => service.UpdateAsync(id, request));
        
        // Assert
        Assert.NotNull(result);
    }
    [Fact]
    public async Task UpdateAsync_WhenNameIsEmpty_ThrowsArgumentException()
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        
        var service = new CategoryService(db);
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var request = new UpdateCategoryRequest
        {
            Name = ""
        };
        
        // Act
        var result = await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateAsync(id, request));
        
        // Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task DeleteAsync_WhenCategoryExists_DeleteCategory()
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        var service = new CategoryService(db);
        var id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        
        // Act
        var result = await service.DeleteAsync(id);
        
        // Assert
        Assert.True(result);
        
        var exists = await db.Categories
            .AnyAsync( x => x.Id == id);
        
        Assert.False(exists);
    }
    
    [Fact]
    public async Task DeleteAsync_WhenCategoryDoesNotExist_ReturnsFalse()
    {
        // Arrange
        await using var db = AppDbContextFactory.Create();
        var service = new CategoryService(db);
        var id = Guid.NewGuid();
        
        // Act
        var result = await service.DeleteAsync(id);
        
        Assert.False(result);
    }
}