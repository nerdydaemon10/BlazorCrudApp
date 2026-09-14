using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlazorCrudApp.Api.Features.Categories;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.HasData(
            new Category
            {
                Id = Guid.Parse("83D0314C-8AAE-4FB1-BB67-3D4C2204E029"),
                Name = "Beverages"
            },
            new Category
            {
                Id = Guid.Parse("993258C6-3C8B-438E-B79D-0033E83E12C5"),
                Name = "Food",
            });
    }
}