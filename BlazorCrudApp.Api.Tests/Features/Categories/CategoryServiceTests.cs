using BlazorCrudApp.Api.Features.Categories;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrudApp.Api.Tests.Features.Categories;

public class CategoryServiceTests
{
    [Fact]
    public async Task GetCategoriesAsync_ReturnsCategories()
    {
        // Arrange
        await using var db = CreateDbContext();
        
        db.Categories.AddRange(
            new Category { Name = "Beverages" },
            new Category { Name = "Snacks" });
        
        var service = new CategoryService(db);
        
        // Act
        var result = await service.GetCategoriesAsync();
        
        // Assert
        Assert.Equal(2, result.Count());
    }
    
    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;
        var db = new AppDbContext(options);
        
        db.Database.OpenConnection();
        db.Database.EnsureCreated();
        
        return db;
    }
}