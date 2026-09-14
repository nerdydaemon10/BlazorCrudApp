namespace BlazorCrudApp.Api.Features.Categories;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/categories")
            .WithTags("Categories");

        group.MapGet("/", GetCategoriesAsync);

        return endpoints;
    }
    private static async Task<IResult> GetCategoriesAsync(CategoryService service, CancellationToken cancellationToken)
    {
        var response = await service.GetCategoriesAsync(cancellationToken);
        return Results.Ok(response);
    }
}