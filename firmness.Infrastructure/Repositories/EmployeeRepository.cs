using firmness.Domain.Entities;
using firmness.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace firmness.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // Obtener todos los empleados (usuarios con rol "Employee")
        public async Task<List<Employee>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var employees = new List<Employee>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Employee"))
                {
                    employees.Add(new Employee
                    {
                        Id = user.Id,
                        Name = user.Name,
                        LastName = user.LastName,
                        Email = user.Email,
                        Phone = user.PhoneNumber,
                        Position = user.Position,
                        Salary = user.Salary
                    });
                }
            }

            return employees;
        }

        // Obtener empleado por ID
        public async Task<Employee?> GetByIdAsync(int id)
        {
            
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("Employee"))
                return null;

            return new Employee
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Position = user.Position,
                Salary = user.Salary
            };
        }

        // Agregar nuevo empleado (no se usa directamente, ver servicio)
        public async Task AddAsync(Employee employee)
        {
            // Este método será manejado por el servicio con UserManager
            await Task.CompletedTask;
        }

        // Actualizar empleado existente
        public async Task UpdateAsync(Employee employee)
        {
            var user = await _userManager.FindByIdAsync(employee.Id.ToString());
            if (user != null)
            {
                user.Name = employee.Name;
                user.LastName = employee.LastName;
                user.Email = employee.Email;
                user.UserName = employee.Email;
                user.PhoneNumber = employee.Phone;
                user.Position = employee.Position;
                user.Salary = employee.Salary;

                await _userManager.UpdateAsync(user);
            }
        }

        // Eliminar empleado por ID
        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        // Guardar cambios (no necesario con UserManager, pero se mantiene por interfaz)
        public async Task SaveAsync()
        {
            await Task.CompletedTask;
        }
    }
}