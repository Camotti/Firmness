using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using firmness.Data.Entities;
using System.Globalization;


namespace firmness.Services
{
    public class PdfService
    {
        public byte[] GenerateSaleReceipt(Sale sale)
        {
            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Header().Text(" Firmness Sales Receipt").Bold().FontSize(20).AlignCenter();
                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Spacing(8);
                        col.Item().Text($"Date: {sale.Date:yyyy-MM-dd}");
                        col.Item().Text($"Client: {sale.Client?.Name ?? "N/A"}");
                        col.Item().Text($"Employee: {sale.Employee?.Name ?? "N/A"}");
                        col.Item().LineHorizontal(1);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(4);
                                c.RelativeColumn(2);
                                c.RelativeColumn(2);
                                c.RelativeColumn(2);
                            });

                            table.Header(h =>
                            {
                                h.Cell().Text("Product").Bold();
                                h.Cell().Text("Qty").Bold();
                                h.Cell().Text("Unit Price").Bold();
                                h.Cell().Text("Subtotal").Bold();
                            });

                            foreach (var d in sale.SaleDetails)
                            {
                                table.Cell().Text(d.Product?.Name ?? "");
                                table.Cell().Text(d.Quantity.ToString());
                                table.Cell().Text(string.Format(CultureInfo.InvariantCulture, "{0:C}", d.UnitPrice));
                                table.Cell().Text(string.Format(CultureInfo.InvariantCulture, "{0:C}", d.Subtotal));
                            }
                        });

                        col.Item().LineHorizontal(1);
                        var total = sale.SaleDetails.Sum(d => d.Subtotal);
                        var iva = total * 0.19m;
                        var grandTotal = total + iva;
                        col.Item().Text($"Subtotal: {total:C}");
                        col.Item().Text($"IVA (19%): {iva:C}");
                        col.Item().Text($"Total: {grandTotal:C}").Bold();
                    });
                    page.Footer().AlignCenter().Text("© 2025 - Firmness System");
                });
            });

            return doc.GeneratePdf();
        }
    }
}




