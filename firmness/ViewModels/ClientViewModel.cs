
using System.ComponentModel.DataAnnotations;

namespace firmness.ViewModels
{    

public class ClientViewModel
{
    public class ClientViewModels
    {
        [Required(ErrorMessage = "The name is obligatory")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The lastname is obligatory")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "The E-mail is obligatory")]
        [EmailAddress(ErrorMessage = " Invalid E-mail")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Phone is obligatory")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "The document is obligatory")]
        [StringLength(20, ErrorMessage = "Maximum 20 characters required")]
        public string Document { get; set; } = string.Empty;

        [Required(ErrorMessage = "The address is obligatory")]
        public string Address { get; set; } = string.Empty;
    }
}

}