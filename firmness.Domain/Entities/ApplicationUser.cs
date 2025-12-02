using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    // Campos comunes (Person)
    [StringLength(100)]
    public string? Name { get; set; }
    
    [StringLength(100)]
    public string? LastName { get; set; }
    
    // Campos específicos de Client
    [StringLength(20)]
    public string? Document { get; set; }
    
    public string? Address { get; set; }
    
    // ⭐ Campos específicos de Employee
    [StringLength(100)]
    public string? Position { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Salary { get; set; }
}