using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using firmness.Domain.Entities;
using firmness.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace firmness.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Para permitir el campo de búsqueda (GET)
        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        // ✅ Lista que mostrará los productos
        public IList<Product> Product { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            var query = _context.Products.AsQueryable();

            // ✅ Si hay texto en el buscador, filtra por nombre o descripción
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(p =>
                    (p.Name ?? "").Contains(SearchTerm) ||
                    (p.Description ?? "").Contains(SearchTerm));
            }
            Product = await query.ToListAsync(); //consulta
        }
    }
}
