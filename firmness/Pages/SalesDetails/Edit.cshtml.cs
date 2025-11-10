using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using firmness.Domain.Entities;
using firmness.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace firmness.Pages.SalesDetails
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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

            var saledetail =  await _context.SaleDetails.FirstOrDefaultAsync(m => m.SaleDetailId == id);
            if (saledetail == null)
            {
                return NotFound();
            }
            SaleDetail = saledetail;
           ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
           ViewData["SaleId"] = new SelectList(_context.Sales, "SaleId", "SaleId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(SaleDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleDetailExists(SaleDetail.SaleDetailId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SaleDetailExists(int id)
        {
            return _context.SaleDetails.Any(e => e.SaleDetailId == id);
        }
    }
}
