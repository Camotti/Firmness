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
    public class DetailsModel : PageModel
    {
        private readonly firmness.Data.ApplicationDbContext _context;

        public DetailsModel(firmness.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public SaleDetail SaleDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saledetail = await _context.SaleDetails.FirstOrDefaultAsync(m => m.SaleDetailId == id);
            if (saledetail == null)
            {
                return NotFound();
            }
            else
            {
                SaleDetail = saledetail;
            }
            return Page();
        }
    }
}
