using firmness.Domain.Entities;
namespace firmness.Application.Interfaces
{
    public interface IFileService
    {
        Task<List<string>> ImportExcelAsync(Stream excelStream);
    }
}
