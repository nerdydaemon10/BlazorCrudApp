using Microsoft.EntityFrameworkCore;

namespace BlazorCrudApp.Api.Features.Categories;

public class CategoryService(AppDbContext db)
{
    public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await db.Categories
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}