namespace OnMed.Persistance.Dtos.Users;

public class UserUpdateDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string MiddleName { get; set; } = String.Empty;
    public DateOnly BirthDay { get; set; }
    public bool IsMale { get; set; }
    public string Region { get; set; } = String.Empty;
}
