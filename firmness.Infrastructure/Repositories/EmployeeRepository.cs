using firmness.Application.Interfaces;
using firmness.Domain.Entities;
using firmness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace firmness.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllAsync() =>
        await _context.Employees.ToListAsync();

    
    public async Task<Employee> GetByIdAsync(int id) =>
        await _context.Employees.FindAsync(id);

    
    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await Task.CompletedTask;
    }


    public async Task DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
            _context.Employees.Remove(employee);
    }

    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();
    
}