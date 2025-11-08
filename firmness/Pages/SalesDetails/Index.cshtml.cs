using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using firmness.Data;
using firmness.Data.Entities;

namespace firmness.Pages.SalesDetails
{
    public class IndexModel : PageModel
    {
        private readonly firmness.Data.ApplicationDbContext _context;

        public IndexModel(firmness.Data.ApplicationDbContext context)
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
