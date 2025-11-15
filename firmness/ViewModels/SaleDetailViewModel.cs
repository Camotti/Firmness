using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace firmness.ViewModels;

public class SaleDetailViewModel
{
    public int SaleDetailId { get; set; }
    
    [Microsoft.Build.Framework.Required]
    public int ProductId { get; set; }
    public List<SelectListItem> Products { get; set; } = new();
    
    
    [Microsoft.Build.Framework.Required]
    [Range(0, 1000)]
    public int Quantity { get; set; }
    
    
    [Required]
    [Range(0, 1000000)]
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;

}