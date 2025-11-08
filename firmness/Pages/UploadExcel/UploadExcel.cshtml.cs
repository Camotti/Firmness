using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using firmness.Interfaces;

namespace firmness.Pages.Files
{
    public class UploadExcelModel : PageModel
    {
        private readonly IFileService _fileService;

        public UploadExcelModel(IFileService fileService)
        {
            _fileService = fileService;
        }

        [BindProperty]
        public IFormFile ExcelFile { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        public string LogFilePath { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ExcelFile == null || ExcelFile.Length == 0)
            {
                ModelState.AddModelError("", "Seleccione un archivo Excel válido.");
                return Page();
            }

            try
            {
                // Llamamos al servicio para procesar el Excel
                Errors = await _fileService.ImportExcelAsync(ExcelFile.OpenReadStream());

                // Guardar log si hay errores
                if (Errors.Any())
                {
                    var logFolder = Path.Combine("wwwroot", "logs");
                    if (!Directory.Exists(logFolder))
                        Directory.CreateDirectory(logFolder);

                    var logFileName = $"ImportLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                    var logPath = Path.Combine(logFolder, logFileName);

                    await System.IO.File.WriteAllLinesAsync(logPath, Errors);

                    LogFilePath = $"/logs/{logFileName}";
                }
            }
            catch (Exception ex)
            {
                Errors.Add($"Ocurrió un error al procesar el archivo: {ex.Message}");
            }

            return Page();
        }
    }
}