using firmness.Infrastructure.Data;
using firmness.Domain.Entities;
using firmness.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace firmness.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync() =>
            await _context.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products.FindAsync(id);

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public  Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
             return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
                _context.Products.Remove(product);
        }

        public async Task SaveAsync() =>
            await _context.SaveChangesAsync();
    }
}