namespace firmness.Application.DTOs;

public class SendReceiptDto
{
    public int SaleId { get; set; }
    public string Email { get; set; } = string.Empty;
}