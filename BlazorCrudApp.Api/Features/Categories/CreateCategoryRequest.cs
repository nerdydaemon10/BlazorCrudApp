namespace BlazorCrudApp.Api.Features.Categories;

public sealed record CreateCategoryRequest
{
    public required string Name { get; init; }
}