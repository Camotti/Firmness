using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firmness.Data.Entities
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SaleId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; } = DateTime.UtcNow; // ✅ Siempre UTC

        // Relaciones
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        // Relación con SaleDetail (uno a muchos)
        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}
