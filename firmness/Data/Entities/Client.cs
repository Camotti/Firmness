using firmness.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace firmness.Data.Entities
{
    public class Client : Person
    {
        [Required, StringLength(20)]
        public string? Document { get; set; }

        [Required]
        public string? Address { get; set; }
    }
}
