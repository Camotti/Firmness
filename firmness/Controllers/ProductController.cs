

using firmness.Application.Interfaces;
using firmness.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace firmness.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        
        // List products 
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
        
        
        //crear un nuevo producto (Get)
        public IActionResult Create()
        {
            return View();
        }

        
        // Crear un nuevo producto con (Post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var createProductDto = new CreateProductDto
            {
                Name = vm.Name,
                Price = vm.Price,
                Stock = vm.Stock,
                Description = vm.Description
            };
            
            var result = await _productService.CreateAsync(createProductDto);
            if (result.Success)
                return RedirectToAction(nameof(Index));
            
            ModelState.AddModelError("", result.Message);
            return View(vm);
        }
        
        // editar producto (get)
        public async Task<IActionResult> Edit(int id)
        {
            var product = (await _productService.GetAllAsync()).FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            var vm = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description
            };
            return View(vm);
        }

        // editar producto post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var product = new product
            {
                Id = vm.Id,
                Name = vm.Name,
                Price = vm.Price,
                Stock = vm.Stock,
                Description = vm.Description
            };

            var result = await _productService.UpdateAsync(product);
            if (result.Success) return RedirectToAction(nameof(Index));
            
            ModelState.AddModelError("", result.Message);
            return View(vm);
        }
        
        //Eliminar producto por Get 
        public async Task<IActionResult> Delete(int id)
        {
            var product = (await _productService.GetAllAsync()).FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
        
        // Eliminar producto por POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
}
