using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace firmness.ViewModels;

public class SaleViewModel
{
    public int  SaleId { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; }=DateTime.UtcNow;
    
    [Required]
    public int ClientId { get; set; }
    
    [Required]
    public int EmployeeId { get; set; }
    
    // listas 
    public List<SelectListItem> Clients { get; set; } = new();
    public List<SelectListItem> Employees { get; set; } = new();
    
    // lista de detalles de la venta 
    public List<SaleDetailViewModel> Details { get; set; } = new();
}

