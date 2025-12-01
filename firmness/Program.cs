using firmness.Application.Interfaces;
using firmness.Application.Services;
using firmness.Domain.Entities;
using firmness.Infrastructure.Data;
using firmness.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using AutoMapper;
using firmness.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar EPPlus para uso no comercial (versión 8+)
// Opción 1: Para uso organizacional
ExcelPackage.License.SetNonCommercialOrganization("firmness");



// 1️⃣ Database connection (PostgreSQL)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));  // Use PostgreSQL provider

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// 2️⃣ Identity configuration (usando IdentityUser estándar)
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // No requiere confirmación por correo
})
.AddRoles<IdentityRole>() // Soporte para roles (Admin, etc.)
.AddEntityFrameworkStores<ApplicationDbContext>();

// 3️⃣ Razor Pages
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());
// generate pdfs
builder.Services.AddScoped<PdfService>();

// 4️⃣ Registro de repositorios y servicios
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); 

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ISalesService, SalesService>(); 
builder.Services.AddScoped<IEmailService, EmailService>(); 

builder.Services.AddScoped<IFileService, FileService>();

// Repositorio  y servicio de ventas
builder.Services.AddScoped<ISalesRepository, SalesRepository>();
builder.Services.AddScoped<ISalesService, SalesService>();
// builder.Services.AddScoped<SalesService>();


// Construcción de la aplicación aws hola
var app = builder.Build();

//registar autoMapper



// Configuración del pipeline HTTP hola people
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Middleware de autenticación
app.UseAuthorization();   // Middleware de autorización

app.MapRazorPages();

// 7️⃣ Inicialización de roles y usuario administrador
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // Crear roles
    string[] roleNames = { "Admin", "Cliente" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Crear administrador
    string adminEmail = "admin@firmess.com";
    string adminPassword = "Admin123.";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var newAdmin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FullUserName = "System Administrator",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(newAdmin, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}

app.Run();