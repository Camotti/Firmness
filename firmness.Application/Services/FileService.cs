using firmness.Application.Interfaces;
using firmness.Domain.Entities;

namespace firmness.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IProductRepository _productRepo;
        private readonly IClientRepository _clientRepo;

        public FileService(IProductRepository productRepo, IClientRepository clientRepo)
        {
            _productRepo = productRepo;
            _clientRepo = clientRepo;
        }

        public async Task<List<string>> ImportExcelAsync(Stream excelStream)
        {
            var errors = new List<string>();

            try
            {
                using var package = new ExcelPackage(excelStream);
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                if (worksheet == null)
                {
                    errors.Add("El archivo no tiene hojas de cálculo.");
                    return errors;
                }

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    string type = worksheet.Cells[row, 1].Text.Trim();

                    switch (type)
                    {
                        case "Producto":
                            var product = new Product
                            {
                                Name = worksheet.Cells[row, 2].Text,
                                Description = worksheet.Cells[row, 3].Text,
                                Price = decimal.TryParse(worksheet.Cells[row, 4].Text, out var p) ? p : 0,
                                Stock = int.TryParse(worksheet.Cells[row, 5].Text, out var s) ? s : 0
                            };
                            await _productRepo.AddAsync(product);
                            break;

                        case "Cliente":
                            var client = new Client
                            {
                                Name = worksheet.Cells[row, 2].Text,
                                LastName = worksheet.Cells[row, 3].Text,
                                Email = worksheet.Cells[row, 4].Text,
                                Phone = worksheet.Cells[row, 5].Text,
                                Document = worksheet.Cells[row, 6].Text,
                                Address = worksheet.Cells[row, 7].Text
                            };
                            await _clientRepo.AddAsync(client);
                            break;

                        default:
                            errors.Add($"Fila {row}: Tipo '{type}' no reconocido.");
                            break;
                    }
                }

                await _productRepo.SaveAsync();
                await _clientRepo.SaveAsync();
            }
            catch (Exception ex)
            {
                errors.Add($"Error durante la importación: {ex.Message}");
            }

            return errors;
        }
    }
}