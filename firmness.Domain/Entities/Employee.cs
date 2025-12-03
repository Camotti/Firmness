using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using firmness.Domain.Models;

namespace firmness.Domain.Entities;
public class Employee : Person
{
    [Required, StringLength(100)]
    public string? Position { get; set; }
    
    [Required, StringLength(50)]
    public string Role { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Salary { get; set; }
}