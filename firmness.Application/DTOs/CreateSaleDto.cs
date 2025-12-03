namespace firmness.Application.DTOs;

public class CreateSaleDto
{
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    public string ClientId { get; set; }
    public string EmployeeId { get; set; }

    public List<CreateSaleDetailDto> Details { get; set; } = new();
}