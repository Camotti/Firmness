using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firmness.Domain.Entities
{
    public class Product
    {
        private int _stock;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string? Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un número válido.")]
        public decimal Price { get; set; }

        public int Stock
        {
            get => _stock;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Product stock cannot be negative");

                _stock = value;
            }
        }

        public string? Description { get; set; }
        public ICollection<SaleDetail>? SaleDetails { get; set; }
    }
}