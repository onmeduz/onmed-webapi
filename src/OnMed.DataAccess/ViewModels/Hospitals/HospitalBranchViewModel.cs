namespace OnMed.DataAccess.ViewModels.Hospitals;

public class HospitalBranchViewModel
{
    public long HospitalId { get; set; }
    public long hospital_branch_id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LegalName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public string BrandImagePath { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public string AdministratorPhoneNumber { get; set; } = string.Empty;
    public string FaxPhoneNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string LicenseGivenDate { get; set; } = string.Empty;
    public string LegalRegisterNumber { get; set; } = string.Empty;
    public string LegalRegisterNumberGivenDate { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public string AdressLatitude { get; set; } = string.Empty;
    public string AdressLongitude { get; set; } = string.Empty;
    public string ContactPhoneNumber { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }


}
