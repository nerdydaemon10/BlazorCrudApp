using BlazorCrudApp.Api;
using BlazorCrudApp.Api.Features.Categories;
using BlazorCrudApp.Api.Features.Products;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
    // options.AddPolicy("Client", policy =>
    // {
    //     policy
    //         .WithOrigins(allowedOrigins)
    //         .AllowAnyHeader()
    //         .AllowAnyMethod();
    // });
    // options.AddPolicy("Admin", policy =>
    // {
    //     policy
    //         .WithOrigins(allowedOrigins)
    //         .AllowAnyHeader()
    //         .AllowAnyMethod();
    // });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// app.MapGet("/products", (AppDbContext db) => db.Products.ToListAsync());
// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");

app.UseCors();
app.MapCategoryEndpoints();
app.MapProductEndpoints();

app.Run();