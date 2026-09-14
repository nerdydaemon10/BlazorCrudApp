using Microsoft.EntityFrameworkCore;

namespace BlazorCrudApp.Api.Features.Products;

public class ProductService(AppDbContext db)
{
    public async Task<IReadOnlyList<Product>> GetProductsAsync(
        CancellationToken cancellationToken = default)
    {
        return await db.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}