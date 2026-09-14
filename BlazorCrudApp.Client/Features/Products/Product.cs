using Radzen;

namespace BlazorCrudApp.Client.Features.Products;

public class Product
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Sku { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Stock { get; init; }
    public int ReorderLevel { get; init; }
    
    public StockStatus Status
    {
        get
        {
            if (Stock > ReorderLevel)
                return StockStatus.InStock;
            if (Stock > 0 && Stock <= ReorderLevel)
                return StockStatus.LowStock;
            return StockStatus.OutOfStock;
        }
    }

    public string StatusIcon => Status switch
    {
        StockStatus.InStock => "check_circle",
        StockStatus.LowStock => "warning",
        StockStatus.OutOfStock => "error",
        _ => throw new ArgumentOutOfRangeException(nameof(Status))
    };
    public BadgeStyle StatusBadgeStyle => Status switch
    {
        StockStatus.InStock => BadgeStyle.Success,
        StockStatus.LowStock => BadgeStyle.Warning,
        StockStatus.OutOfStock => BadgeStyle.Danger,
        _ => throw new ArgumentOutOfRangeException(nameof(Status))
    };
}