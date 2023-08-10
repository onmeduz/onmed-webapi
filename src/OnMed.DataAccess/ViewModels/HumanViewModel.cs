namespace OnMed.DataAccess.ViewModels;

public class HumanViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public DateOnly BirthDay { get; set; }
    public bool IsMale { get; set; }
    public string ImagePath { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
