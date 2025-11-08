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
    public class DeleteModel : PageModel
    {
        private readonly firmness.Data.ApplicationDbContext _context;

        public DeleteModel(firmness.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saledetail = await _context.SaleDetails.FindAsync(id);
            if (saledetail != null)
            {
                SaleDetail = saledetail;
                _context.SaleDetails.Remove(SaleDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
