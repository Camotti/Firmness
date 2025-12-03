namespace firmness.Application.DTOs;

public class UpdateSaleDto
{
    public int Id { get; set; }
    public DateTime SaleDate { get; set; }
    public string ClientId { get; set; }
    public string EmployeeId { get; set; }
    public List<UpdateSaleDetailDto> Details { get; set; } = new();
}