namespace firmness.Application.DTOs;

public class UpdateSaleDto
{
    public DateTime SaleDate { get; set; }
    public int ClientId { get; set; }
    public int EmployeeId { get; set; }
}