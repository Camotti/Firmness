using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firmness.Domain.Entities
{
    public class SaleDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleDetailId { get; set; } //clave primaria


        [Required]
        public int Quantity { get; set; }


        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }



        [NotMapped] // Calculado en código, no en base de datos
        public decimal Subtotal => Quantity * UnitPrice;



        // Relaciones
        public int SaleId { get; set; } // llave foranea
        public Sale? Sale { get; set; }

        public int ProductId { get; set; } //llave foranea
        public Product? Product { get; set; }
    }
}

