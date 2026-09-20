using Microsoft.EntityFrameworkCore;

namespace BlazorCrudApp.Api.Features.Categories;

public class CategoryService(AppDbContext db)
{
    public async Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await db.Categories
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ThenBy(x => x.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await db.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public async Task<Category> CreateAsync(
        CreateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var exists = await db.Categories
            .AnyAsync(x => x.Name == request.Name, cancellationToken);
        
        if (exists)
            throw new ConflictException(
                "A category with this name already exists.",
                nameof(CreateCategoryRequest.Name));
        
        var category = new Category(request.Name);
        
        db.Categories.Add(category);
        
        await db.SaveChangesAsync(cancellationToken);
        
        return category;
    }
    
    public async Task<bool> UpdateAsync(
        Guid id,
        UpdateCategoryRequest request,
        CancellationToken cancellationToken = default)
    {
        var category = await db.Categories
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (category is null)
            return false;

        var nameExists = await db.Categories
            .AnyAsync(x => x.Name == request.Name, cancellationToken);
        
        if (nameExists)
            throw new ConflictException(
                "A category with this name already exists.",
                nameof(UpdateCategoryRequest.Name));
        
        category.Rename(request.Name);
        
        await db.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var category = await db.Categories
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (category is null)
            return false;

        db.Categories.Remove(category);

        await db.SaveChangesAsync(cancellationToken);

        return true;
    }
}