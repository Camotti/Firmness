namespace firmness.Application.DTOs;

public class ReceiptDto
{
    public int SaleId { get; set; }
    public string? Email { get; set; }
    public List<ItemReceiptDto> Items { get; set; }
    public decimal Total { get; set; }
}