using firmness.Domain.Entities;

namespace firmness.Application.Interfaces;

public interface IEmployeeService
{
    Task<List<Employee>> GetAllAsync();
    Task<(bool success, string Message)> CreateAsync(Employee employee);
    Task<(bool success, string Message)> UpdateAsync(Employee employee);
    Task<(bool success, string Message)> DeleteAsync(int id);
}