using firmness.Application.DTOs;
using firmness.Application.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace firmness.Api.Controllers;


    [ApiController]
    [Route ("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    //Get api of CLient 
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _clientService.GetAllAsync();
        return Ok(clients);
    }
    
    //Post api / client 
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _clientService.CreateAsync(dto);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateClientDto dto)
    {
        if (id != dto.Id)  // 
            return BadRequest("The Id in the URL doesn't match the DTO");
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _clientService.UpdateAsync(dto);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Message);
    }
    
    //Delete api clients by ID 
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _clientService.DeleteAsync(id);

        if (!result.Success)
            return NotFound(result.Message);

        return Ok(result.Message);
    }
    
}