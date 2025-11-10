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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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
