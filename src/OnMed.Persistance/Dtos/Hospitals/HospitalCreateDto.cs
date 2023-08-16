using Microsoft.AspNetCore.Http;

namespace OnMed.Persistance.Dtos.Hospitals;

public class HospitalCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string LegalName { get; set; } = string.Empty;
    public IFormFile BrandImage { get; set; } = default!;
    public string AdministratorPhoneNumber { get; set; } = string.Empty;
    public string FaxPhoneNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public DateOnly LicenseGivenDate { get; set; }
    public string LegalRegisterNumber { get; set; } = string.Empty;
    public DateOnly LegalRegisterNumberGivenDate { get; set; }
}
