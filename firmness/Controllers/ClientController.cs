
using firmness.Application.DTOs;
using firmness.Application.Interfaces;
using firmness.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace firmness.Controllers;

public class ClientController: Controller
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    public async Task<IActionResult> Index()
    {
        var clients = await _clientService.GetAllAsync();
        return View(clients);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(ClientViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var dto = new CreateClientDto()
        {
            Name = model.Name,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
            Document = model.Document,
            Address = model.Address
        };

        var result = await _clientService.CreateAsync(dto);
        if (!result.Success)
        {
            ModelState.AddModelError("", result.Message);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string id)
    {
        var clients = await _clientService.GetAllAsync();
        var client = clients.FirstOrDefault(c => c.Id == id);
        if (client == null) return NotFound();

        var vm = new ClientViewModel
        {
            Name = client.Name!,
            LastName = client.LastName!,
            Email = client.Email!,
            Phone = client.Phone!,
            Document = client.Document!,
            Address = client.Address!
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(string id, ClientViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var dto = new UpdateClientDto()
        {
            Id = id,
            Name = model.Name,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
            Document = model.Document,
            Address = model.Address
        };
        var result = await _clientService.UpdateAsync(dto);
        if (!result.Success)
        {
            ModelState.AddModelError("", result.Message);
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string id)
    {
        var result = await _clientService.DeleteAsync(id);
        if (!result.Success)
            return BadRequest(result.Message);

        return RedirectToAction(nameof(Index));
    }

}
