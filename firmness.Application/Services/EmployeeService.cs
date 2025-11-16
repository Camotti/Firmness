using firmness.Application.Interfaces;
using firmness.Domain.Entities;

namespace firmness.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repo;

    public EmployeeService(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Employee>> GetAllAsync() =>
        await _repo.GetAllAsync();

    public async Task<(bool Success, string Message)> CreateAsync(Employee employee)
    {
        try
        {
            await _repo.AddAsync(employee);
            await _repo.SaveAsync();
            return (true, "Empleado creado correctamente.");
        }
        catch (Exception ex)
        {
            return (false, $"Error: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> UpdateAsync(Employee employee)
    {
        try
        {
            await _repo.UpdateAsync(employee);
            await _repo.SaveAsync();
            return (true, "Empleado actualizado.");
        }
        catch (Exception ex)
        {
            return (false, $"Error: {ex.Message}");
        }
    }

    public async Task<(bool Success, string Message)> DeleteAsync(int id)
    {
        try
        {
            await _repo.DeleteAsync(id);
            await _repo.SaveAsync();
            return (true, "Empleado eliminado.");
        }
        catch (Exception ex)
        {
            return (false, $"Error: {ex.Message}");
        }
    }
}
