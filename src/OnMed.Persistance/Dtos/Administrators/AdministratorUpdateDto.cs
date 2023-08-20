using Microsoft.AspNetCore.Http;

namespace OnMed.Persistance.Dtos.Administrators;

public class AdministratorUpdateDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string MiddleName { get; set; } = String.Empty;
    public IFormFile Image { get; set; } = default!;
    public string Region { get; set; } = String.Empty;
}
