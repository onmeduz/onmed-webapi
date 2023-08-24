namespace OnMed.DataAccess.ViewModels.Administrators;

public class AdministratorViewModel
{
    public long AdminId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }
    public long[] HospitalBranchIds { get; set; } = { };
    public string[] HospitalNames { get; set; } = { };
}
