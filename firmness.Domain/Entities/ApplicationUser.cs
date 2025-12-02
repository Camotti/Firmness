// csharp
// Archivo: `firmness.Domain/Entities/ApplicationUser.cs`
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? LastName { get; set; }

    [StringLength(20)]
    public string? Document { get; set; }

    public string? Address { get; set; }

    [StringLength(100)]
    public string? Position { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Salary { get; set; }

    // No mapeada a BD; permite asignar FullUserName y mantiene Name/LastName sincronizados
    [NotMapped]
    public string FullUserName
    {
        get => $"{(Name ?? string.Empty).Trim()} {(LastName ?? string.Empty).Trim()}".Trim();
        set
        {
            var parts = (value ?? string.Empty).Trim().Split(' ', 2);
            Name = parts.Length > 0 ? parts[0] : string.Empty;
            LastName = parts.Length > 1 ? parts[1] : string.Empty;
        }
    }
}