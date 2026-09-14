using System.ComponentModel.DataAnnotations;

namespace BlazorCrudApp.Client.Features.Products;

public enum StockStatus
{
    [Display(Name = "In Stock")]
    InStock,
    [Display(Name = "Low Stock")]
    LowStock,
    [Display(Name = "Out of Stock")]
    OutOfStock
}