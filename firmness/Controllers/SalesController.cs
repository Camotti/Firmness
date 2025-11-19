using firmness.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using firmness.Application.Interfaces;
using firmness.ViewModels;
using firmness.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace firmness.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService _salesService;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;
        private readonly IEmployeeService _employeeService;

        public SalesController(
            ISalesService salesService,
            IProductService productService,
            IClientService clientService,
            IEmployeeService employeeService)
        {
            _salesService = salesService;
            _productService = productService;
            _clientService = clientService;
            _employeeService = employeeService;
        }

        
        //        LISTAR
        
        public async Task<IActionResult> Index()
        {
            var sales = await _salesService.GetAllSalesAsync();
            return View(sales);
        }

        
        //     CREAR - GET
        
        public async Task<IActionResult> Create()
        {
            var vm = new SaleViewModel
            {
                Clients = (await _clientService.GetAllAsync())
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList(),

                Employees = (await _employeeService.GetAllAsync())
                    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
                    .ToList(),

                Products = (await _productService.GetAllAsync())
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToList(),

                Details = new List<SaleDetailViewModel>()
            };

            return View(vm);
        }

        
        //     CREAR - POST
        
        [HttpPost]
        public async Task<IActionResult> Create(SaleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return await ReloadData(vm);
            }

            var sale = new CreateSaleDto()
            {
                ClientId = vm.ClientId,
                EmployeeId = vm.EmployeeId,
                SaleDate  = DateTime.UtcNow,
                Details = vm.Details.Select(d => new SaleDetailDto()
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice,
                    
                }).ToList()
            };

            await _salesService.CreateSaleAsync(sale);

            return RedirectToAction("Index");
        }

        // ========================
        //      EDITAR - GET
        // ========================
        public async Task<IActionResult> Edit(int id)
        {
            var sale = await _salesService.GetSaleByIdAsync(id);
            if (sale == null) return NotFound();

            var vm = new SaleViewModel
            {
                SaleId = sale.SaleId,
                ClientId = sale.ClientId,
                EmployeeId = sale.EmployeeId,

                Clients = (await _clientService.GetAllAsync())
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                    .ToList(),

                Employees = (await _employeeService.GetAllAsync())
                    .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
                    .ToList(),

                Products = (await _productService.GetAllAsync())
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                    .ToList(),

                Details = vm.Details.Select(d => new SaleDetailDto()
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList()
            };

            return View(vm);
        }

        // ========================
        //       EDITAR - POST
        // ========================
        [HttpPost]
        public async Task<IActionResult> Edit(SaleViewModel vm)
        {
            if (!ModelState.IsValid)
                return await ReloadData(vm);

            var sale = await _salesService.GetSaleByIdAsync(vm.SaleId);
            if (sale == null) return NotFound();

            sale.ClientId = vm.ClientId;
            sale.EmployeeId = vm.EmployeeId;

            sale.SaleDetails = vm.Details.Select(d => new SaleDetail
            {
                SaleId = sale.SaleId,
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice,
                
            }).ToList();

            await _salesService.UpdateSaleAsync(sale);

            return RedirectToAction("Index");
        }

        // ========================
        //        ELIMINAR
        // ========================
        public async Task<IActionResult> Delete(int id)
        {
            var sale = await _salesService.GetSaleByIdAsync(id);
            if (sale == null) return NotFound();

            return View(sale);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _salesService.DeleteSaleAsync(id);
            return RedirectToAction("Index");
        }

        // ========================
        // Recarga listas si el modelo falla
        // ========================
        private async Task<IActionResult> ReloadData(SaleViewModel vm)
        {
            vm.Clients = (await _clientService.GetAllAsync())
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

            vm.Employees = (await _employeeService.GetAllAsync())
                .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
                .ToList();

            vm.Products = (await _productService.GetAllAsync())
                .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name })
                .ToList();

            return View("Create", vm);
        }
    }
}
