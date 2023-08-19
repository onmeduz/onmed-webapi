using Microsoft.AspNetCore.Http;

namespace OnMed.Persistance.Dtos.Administrators;

public class AdministratorCreateDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string MiddleName { get; set; } = String.Empty;
    public DateOnly BirthDay { get; set; }
    public string PhoneNumber { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public bool IsMale { get; set; }
    public IFormFile Image { get; set; } = default!;
    public string Region { get; set; } = String.Empty;
    public long HospitalId { get; set; }
}
