namespace firmness.Application.DTOs;

public class UpdateEmployeeDto
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Position { get; set; }
    public string? Role { get; set; }
    public decimal? Salary { get; set; }
}