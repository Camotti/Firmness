public class CreateClientDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Document { get; set; }
    public string Address { get; set; }
    
    // ⭐ NUEVO: necesario para crear usuario
    public string? Password { get; set; }
}

