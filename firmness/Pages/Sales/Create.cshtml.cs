
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using firmness.Domain.Entities;
using firmness.Infrastructure.Data;

namespace firmness.Pages.Sales
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sale Sale { get; set; } = new Sale();

        public SelectList ClientList { get; set; } = default!;
        public SelectList EmployeeList { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var clients = await _context.Clients
                    .Select(c => new { c.Id, c.Name })
                    .ToListAsync();

                var employees = await _context.Employees
                    .Select(e => new { e.Id, e.Name })
                    .ToListAsync();

                ClientList = new SelectList(clients, "Id", "Name");
                EmployeeList = new SelectList(employees, "Id", "Name");


                ViewData["ClientId"] = ClientList;
                ViewData["EmployeeId"] = EmployeeList;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al cargar los datos: {ex.Message}");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // 1) validar modelo básico
            if (!ModelState.IsValid)
            {
                await LoadSelectListsAsync();
                return Page();
            }

            // 2) validar que los ids referenciados existan en BD antes de intentar guardar
            var clientExists = await _context.Clients.AnyAsync(c => c.Id == Sale.ClientId);
            var employeeExists = await _context.Employees.AnyAsync(e => e.Id == Sale.EmployeeId);

            if (!clientExists)
            {
                ModelState.AddModelError("", $"Client with Id {Sale.ClientId} does not exist.");
                await LoadSelectListsAsync();
                return Page();
            }

            if (!employeeExists)
            {
                ModelState.AddModelError("", $"Employee with Id {Sale.EmployeeId} does not exist.");
                await LoadSelectListsAsync();
                return Page();
            }

            try
            {
                _context.Sales.Add(Sale);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Sale created successfully.";
                return RedirectToPage("/Sales/Index");
            }
            catch (DbUpdateException dbEx)
            {
                // Mostrar inner exception si existe para diagnóstico
                var inner = dbEx.InnerException?.Message ?? dbEx.Message;
                ModelState.AddModelError("", $"Database update error: {inner}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Unexpected error: {ex.Message}");
            }

            await LoadSelectListsAsync();
            return Page();
        }

        private async Task LoadSelectListsAsync()
        {
            var clients = await _context.Clients
                .Select(c => new { c.Id, c.Name })
                .ToListAsync();

            var employees = await _context.Employees
                .Select(e => new { e.Id, e.Name })
                .ToListAsync();

            ViewData["ClientId"] = new SelectList(clients, "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(employees, "Id", "Name");

        }
    }
}