using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firmness.Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string? Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un número válido.")]
        public decimal Price { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un número entero positivo.")]
        public int Stock { get; set; }


        public string? Description { get; set; }

        //esta es la relacion con SaleDetail
        public ICollection<SaleDetail>? SaleDetails { get; set; }
    }
}
