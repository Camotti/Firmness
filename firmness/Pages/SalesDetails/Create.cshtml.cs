using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using firmness.Data;
using firmness.Data.Entities;

namespace firmness.Pages.SalesDetails
{
    public class CreateModel : PageModel
    {
        private readonly firmness.Data.ApplicationDbContext _context;

        public CreateModel(firmness.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
        ViewData["SaleId"] = new SelectList(_context.Sales, "SaleId", "SaleId");
            return Page();
        }

        [BindProperty]
        public SaleDetail SaleDetail { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SaleDetails.Add(SaleDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
