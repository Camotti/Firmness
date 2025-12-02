using firmness.Application.Interfaces;
using firmness.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace firmness.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeService(
            IEmployeeRepository repo,
            UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }

        public async Task<List<Employee>> GetAllAsync() =>
            await _repo.GetAllAsync();

        public async Task<(bool Success, string Message)> CreateAsync(Employee employee)
        {
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(employee.Name) || string.IsNullOrWhiteSpace(employee.Email))
                    return (false, "El nombre y el correo son obligatorios.");

                if (!employee.Email.Contains("@"))
                    return (false, "El correo electrónico no es válido.");

                // Verificar si el usuario ya existe
                var existingUser = await _userManager.FindByEmailAsync(employee.Email);
                if (existingUser != null)
                    return (false, "Ya existe un usuario con este correo electrónico.");

                // Crear el ApplicationUser
                var user = new ApplicationUser
                {
                    UserName = employee.Email,
                    Email = employee.Email,
                    Name = employee.Name,
                    LastName = employee.LastName,
                    PhoneNumber = employee.Phone,
                    Position = employee.Position,
                    Salary = employee.Salary
                };

                // Crear usuario con contraseña temporal
                // Idealmente deberías recibir la contraseña en el Employee o crear un DTO
                string temporaryPassword = GenerateTemporaryPassword();
                var result = await _userManager.CreateAsync(user, temporaryPassword);

                if (!result.Succeeded)
                    return (false, $"Error al crear empleado: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                // Asignar rol de Employee
                await _userManager.AddToRoleAsync(user, "Employee");

                return (true, $"Empleado creado correctamente. Contraseña temporal: {temporaryPassword}");
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
                var user = await _userManager.FindByIdAsync(employee.Id.ToString());
                if (user == null)
                    return (false, "Empleado no encontrado.");

                // Verificar que sea un empleado
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Employee"))
                    return (false, "El usuario no es un empleado.");

                // Actualizar datos
                user.Name = employee.Name;
                user.LastName = employee.LastName;
                user.Email = employee.Email;
                user.UserName = employee.Email;
                user.PhoneNumber = employee.Phone;
                user.Position = employee.Position;
                user.Salary = employee.Salary;

                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                    return (false, $"Error al actualizar: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                return (true, "Empleado actualizado correctamente.");
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
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                    return (false, "Empleado no encontrado.");

                // Verificar que sea un empleado
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Employee"))
                    return (false, "El usuario no es un empleado.");

                var result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                    return (false, $"Error al eliminar: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                return (true, "Empleado eliminado correctamente.");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        // Método auxiliar para generar contraseña temporal
        private string GenerateTemporaryPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}