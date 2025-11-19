namespace firmness.Application.DTOs;

public class UpdateSaleDto
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }
    public int ClientId { get; set; }
    public int EmployeeId { get; set; }
}