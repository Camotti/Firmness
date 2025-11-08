using firmness.Models;
using System.ComponentModel.DataAnnotations;

namespace firmness.Data.Entities
{
    public class Employee : Person 
    {
        
        [Required(ErrorMessage = "El rol es obligatorio.")]
        public string? Role { get; set; } //vendedor o manager

        [Range(0, 10000000, ErrorMessage = "El salario debe ser un número positivo.")]
        [DataType(DataType.Currency)]
        public decimal? Salary { get; set; }
    }
}
