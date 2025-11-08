using System.ComponentModel.DataAnnotations;
using firmness.Domain.Models;
namespace firmness.Domain.Entities
{
    public class Client : Person
    {
        [Required, StringLength(20)]
        public string? Document { get; set; }

        [Required]
        public string? Address { get; set; }
    }
}
