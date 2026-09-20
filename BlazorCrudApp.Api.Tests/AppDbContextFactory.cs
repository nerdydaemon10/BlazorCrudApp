using Microsoft.EntityFrameworkCore;

namespace BlazorCrudApp.Api.Tests;

public static class AppDbContextFactory
{
    public static AppDbContext Create()
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