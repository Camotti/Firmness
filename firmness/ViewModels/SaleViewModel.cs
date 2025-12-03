using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace firmness.ViewModels;

public class SaleViewModel
{
    public int SaleId { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; }=DateTime.UtcNow;
    
    [Required]
    public string ClientId { get; set; }
    
    [Required]
    public string EmployeeId { get; set; }
    
    // listas 
    public List<SelectListItem> Clients { get; set; } = new();
    public List<SelectListItem> Employees { get; set; } = new();
    public List<SelectListItem> Products { get; set; } = new(); 
    
    // lista de detalles de la venta 
    public List<SaleDetailViewModel> Details { get; set; } = new();
}

