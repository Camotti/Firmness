using Microsoft.AspNetCore.Mvc.RazorPages;
using firmness.Data;
using Microsoft.EntityFrameworkCore;

namespace firmness.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int TotalClients { get; set; }
        public int TotalEmployees { get; set; }
        public int TotalProducts { get; set; }
        public int TotalSales { get; set; }

        public async Task OnGetAsync()
        {
            TotalClients = await _context.Clients.CountAsync();
            TotalEmployees = await _context.Employees.CountAsync();
            TotalProducts = await _context.Products.CountAsync();
            TotalSales = await _context.Sales.CountAsync();
        }
    }
}