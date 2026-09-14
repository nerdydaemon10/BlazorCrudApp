namespace BlazorCrudApp.Api.Features.Products;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/products")
            .WithTags("Products");
        
        // Use Admin Cors Policy
        // var group = endpoints.MapGroup("/api/products")
        //     .WithTags("Products")
        //     .RequireCors("Admin");
        
        group.MapGet("/", GetProductsAsync);
        
        return endpoints;
    }
    private static async Task<IResult> GetProductsAsync(
        ProductService service,
        CancellationToken cancellationToken)
    {
        var products = await service.GetProductsAsync(cancellationToken);
        return Results.Ok(products);
    }
}