using System.ComponentModel.DataAnnotations;

namespace firmness.Domain.Models
{
    public abstract class Person
    {
        public int Id { get; set; }


        [Required, StringLength(100)]
        public string? Name { get; set; }


        [Required, StringLength(100)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Número de teléfono inválido.")]
        public string? Phone { get; set; }
    }   
}
