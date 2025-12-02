using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using firmness.Domain.Models;

public class Employee : Person
{
    [Required, StringLength(100)]
    public string? Position { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Salary { get; set; }
}