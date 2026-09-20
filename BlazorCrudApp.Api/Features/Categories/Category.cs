using System.Text.Json.Serialization;

namespace BlazorCrudApp.Api.Features.Categories;

public class Category
{
    [JsonInclude]
    public Guid Id { get; private set; }
    [JsonInclude]
    public string Name { get; private set; } = string.Empty;
    
    public Category() {}
    
    public Category(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public void Rename(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }
}