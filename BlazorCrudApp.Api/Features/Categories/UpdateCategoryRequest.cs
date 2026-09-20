namespace BlazorCrudApp.Api.Features.Categories;

public sealed record UpdateCategoryRequest
{
    public required string Name { get; init; }
}