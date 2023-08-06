using System.ComponentModel.DataAnnotations;

namespace OnMed.Domain.Entities.Hospitals;

public class Hospital : Auditable
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(50)]
    public string LegalName { get; set; } = string.Empty;

    public string BrandImagePath { get; set; } = string.Empty;

    [MaxLength(13)]
    public string AdministratorPhoneNumber { get; set; } = string.Empty;

    [MaxLength(13)]
    public string FaxPhoneNumber { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Emile { get; set; } = string.Empty;

    public string Website { get; set; } = string.Empty;

    public string LicenseNumber { get; set; } = string.Empty;

    public DateOnly LicenseGivenDate { get; set; }

    public string LegalRegisterNumber { get; set; } = string.Empty;

    public DateOnly LegalRegisterNumberGivenDate { get; set; }
}
