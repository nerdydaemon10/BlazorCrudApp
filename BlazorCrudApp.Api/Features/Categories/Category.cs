namespace BlazorCrudApp.Api.Features.Categories;

public class Category
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
}