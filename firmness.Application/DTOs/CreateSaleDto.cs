namespace firmness.Application.DTOs;

public class CreateSaleDto
{
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    public int ClientId { get; set; }
    public int EmployeeId { get; set; }

    public List<CreateSaleDetailDto> Details { get; set; } = new();
}