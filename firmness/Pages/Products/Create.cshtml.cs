using firmness.Application.DTOs;
using firmness.Application.Interfaces;
using firmness.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace firmness.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;

        public CreateModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
        public Product Product { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var (success, message) = await _productService.CreateAsync(new CreateProductDto());
            TempData["Message"] = message;

            if (!success)
                return Page();

            return RedirectToPage("./Index");
        }
    }
}
