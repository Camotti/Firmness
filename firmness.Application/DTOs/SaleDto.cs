

namespace firmness.Application.DTOs;

public class SaleDto
{
    public int SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public int ClientId { get; set; }
    public int EmployeeId { get; set; }

    public List<SaleDetailDto> Details { get; set; } = new();
}