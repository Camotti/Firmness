using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using firmness.Data;
using firmness.Data.Entities;

namespace firmness.Pages.Sales
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Sale> Sale { get; set; } = new List<Sale>();

        public async Task OnGetAsync()
        {
            // ✅ Incluimos las relaciones Client y Employee
            Sale = await _context.Sales
                .Include(s => s.Client)
                .Include(s => s.Employee)
                .ToListAsync();
        }
    }
}
