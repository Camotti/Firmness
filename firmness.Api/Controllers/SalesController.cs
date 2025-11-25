using firmness.Application.DTOs;
using firmness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace firmness.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _saleService;

        public SalesController(ISalesService salesService)
        {
            _saleService = salesService;
        }
        
        
        //Get esta es la APi 
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sales = await _saleService.GetAllSalesAsync();
            return Ok(sales);

        }
        
        
        // Get Sales by Id
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            
            if (sale == null)
                return NotFound($"Sale with {id} not found");

            return Ok(sale);
        }
        
        //POST: API Sales
        [Authorize(Roles = "client")]
        [HttpPost("purchase")]
        public async Task<IActionResult> Purchase([FromBody] CreateSaleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _saleService.CreateSaleAsync(dto);
            if (!success)
                return BadRequest("Could not create the sale");
            return Ok("Sale created successfully");
        }
        
        //POST : api/sales/send-receipt
        [Authorize(Roles = "client")]
        [HttpPost("send-receipt")]
        public async Task<IActionResult> SendReceipt([FromBody] SendReceiptDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var success = await _saleService.SendReceiptAsync(dto);
            if (!success)
                return BadRequest("Could not send receipt");
            return Ok("Receipt sent successfully");
        }
        
        
        //Put: api / sales by Id
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] UpdateSaleDto dto)
        {
            if (id != dto.Id)
                return BadRequest("The Id in the URL doesn't match the DTO");

            var success = await _saleService.UpdateSaleAsync(dto);
            if (!success)
                return BadRequest("Could not update sale");

            return Ok("Sale Updated successfully");
        }
        
        //Delete api / sales / clients
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _saleService.DeleteSaleAsync(id);
            if (!success)
                return NotFound("Sale not found");
            return Ok("Sale deleted successfully");
        }
        
        //Get: api sales clients 
        [Authorize(Roles = "Admin")]
        [HttpGet("clients")]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _saleService.GetClientsAsync();
            return Ok(clients);
        }
        
        // GEt : api sales employees
        [Authorize(Roles = "Admin")]
        [HttpGet("employees")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _saleService.GetEmployeesAsync();
            return Ok(employees);
        }
        
        // GEt: api/sales products 
        [Authorize(Roles = "Admin")]
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _saleService.GetProductsAsync();
            return Ok(products);
        }
        
    }

}