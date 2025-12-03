

namespace firmness.Application.DTOs;

public class SaleDto
{
    public int SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public string ClientId { get; set; }
    public string EmployeeId { get; set; }

    public List<SaleDetailDto> Details { get; set; } = new();
}