namespace firmness.Application.DTOs;

public class SaleDetailDto
{
    public int SaleDetailId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal TotalPrice => Quantity * UnitPrice; // se puede borrar 
}