using firmness.Application.DTOs;
using firmness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace firmness.Api.Controllers

{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        
        //Get All 
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        /// Create

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            var result = await _productService.CreateAsync(dto);
            if (!result.Success)
                return BadRequest(new { message = result.Message});

            return Ok(new { message = result.Message });
        }
            
        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { Message = "The URL's Id doesn't match" });

            var result = await _productService.UpdateAsync(dto);
            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }
        
        //Delete 
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result.Success)
                return NotFound(new { message = result.Message });

            return Ok(new { message = result.Message });
        }
        
        

    }

}