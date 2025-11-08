using Microsoft.AspNetCore.Http;


namespace firmness.Interfaces
{
    public interface IFileService
    {
        Task<List<string>> ImportExcelAsync(Stream excelStream);
    }
}
