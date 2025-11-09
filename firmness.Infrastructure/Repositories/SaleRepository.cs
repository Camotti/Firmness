using firmness.Infrastructure.Data;
using firmness.Infrastructure.Data;
using firmness.Domain.Entities;
using firmness.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace firmness.Infrastructure.Repositories
{
    public class SalesRepository
    {
        private readonly ApplicationDbContext _context;

        public SalesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Obtener todas las ventas
        public async Task<List<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.Client)
                .Include(s => s.Employee)
                .Include(s => s.SaleDetails)
                .ThenInclude(d => d.Product)
                .ToListAsync();
        }

        // ✅ Obtener una venta por ID
        public async Task<Sale?> GetByIdAsync(int id)
        {
            return await _context.Sales
                .Include(s => s.Client)
                .Include(s => s.Employee)
                .Include(s => s.SaleDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(s => s.SaleId == id);
        }

        // ✅ Crear una nueva venta
        public async Task AddAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        // ✅ Actualizar una venta existente
        public async Task UpdateAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }

        // ✅ Eliminar una venta
        public async Task DeleteAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
        }

        // ✅ Obtener listas auxiliares
        public async Task<List<Client>> GetClientsAsync()
        {
            return await _context.Clients.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.OrderBy(e => e.Name).ToListAsync();
        }
    }
}
