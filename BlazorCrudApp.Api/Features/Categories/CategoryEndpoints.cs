namespace BlazorCrudApp.Api.Features.Categories;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/categories")
            .WithTags("Categories");
        
        group.MapGet("/", GetCategories);
        group.MapGet("/{id:guid}", GetCategory);
        group.MapPost("/", CreateCategory)
            .AddEndpointFilter<ValidationFilter<CreateCategoryRequest>>();
        group.MapPut("/{id:guid}", UpdateCategory)
            .AddEndpointFilter<ValidationFilter<UpdateCategoryRequest>>();
        group.MapDelete("/{id:guid}", DeleteCategory);
        
        return endpoints;
    }
    private static async Task<IResult> GetCategories(
        CategoryService service, 
        CancellationToken cancellationToken)
    {
        var response = await service.GetAllAsync(cancellationToken);
        return Results.Ok(response);
    }

    private static async Task<IResult> GetCategory(
        Guid id,
        CategoryService service,
        CancellationToken cancellationToken)
    {
        var category = await service.GetByIdAsync(
            id,
            cancellationToken);

        return category is null
            ? Results.NotFound()
            : Results.Ok(category);
    }
    
    private static async Task<IResult> CreateCategory(
        CreateCategoryRequest request,
        CategoryService service,
        CancellationToken cancellationToken)
    {
        var category = await service.CreateAsync(
            request, 
            cancellationToken);

        return Results.Created(
            $"/api/categories/{category.Id}",
            category);
    }
    
    private static async Task<IResult> UpdateCategory(
        Guid id,
        UpdateCategoryRequest request,
        CategoryService service,
        CancellationToken cancellationToken)
    {
        var updated = await service.UpdateAsync(id, request, cancellationToken);
        
        return updated
            ? Results.NoContent()
            : Results.NotFound();
    }

    private static async Task<IResult> DeleteCategory(
        Guid id,
        CategoryService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        
        return deleted
            ? Results.NoContent()
            : Results.NotFound();
    }
}