using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using firmness.Domain.Entities;
using firmness.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace firmness.Pages.SalesDetails
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SaleDetail> SaleDetail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            SaleDetail = await _context.SaleDetails
                .Include(s => s.Product)
                .Include(s => s.Sale).ToListAsync();
        }
    }
}
