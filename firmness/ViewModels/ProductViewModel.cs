using System.ComponentModel.DataAnnotations;

namespace firmness.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name must be between 0 and 100 characters long.")]
        public string Name { get; set; } = string.Empty;
        
        
        [Range(0, 1000000, ErrorMessage = "The price must be between 0 and 100 characters long.")]
        public decimal Price { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Price must be bigger or equal to 0.")]
        public int Stock { get; set; }
        
        [StringLength(300, ErrorMessage = "The description shouldn't be longer than 300 characters long.")]
        public string? Description { get; set; }
        
    }

}