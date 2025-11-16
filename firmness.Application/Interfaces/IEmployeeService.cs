using firmness.Domain.Entities;

namespace firmness.Application.Interfaces;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();
    Task<(bool Success, string Message)> CreateAsync(Employee employee);
    Task<(bool Success, string Message)> UpdateAsync(Employee employee);
    Task<(bool Success, string Message)> DeleteAsync(int id);
}