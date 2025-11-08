using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using System.Data;

namespace firmness.Pages.Excel
{
    public class ImportModel : PageModel
    {
        [BindProperty]
        public IFormFile? UploadedFile { get; set; }

        public string? Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                Message = "Por favor, selecciona un archivo Excel válido.";
                return Page();
            }

            try
            {
                // ✅ EPPlus 8+: configurar licencia correctamente
                ExcelPackage.License.SetNonCommercialOrganization("Firmness"); // o usa SetNonCommercialPersonal("Pietro")

                using var stream = new MemoryStream();
                await UploadedFile.CopyToAsync(stream);
                using var package = new ExcelPackage(stream);

                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                {
                    Message = "El archivo no contiene hojas válidas.";
                    return Page();
                }

                int totalRows = worksheet.Dimension.Rows;
                Message = $" Archivo importado correctamente. Filas detectadas: {totalRows}";
            }
            catch (Exception ex)
            {
                Message = $" Error al procesar el archivo: {ex.Message}";
            }

            return Page();
        }
    }
}
