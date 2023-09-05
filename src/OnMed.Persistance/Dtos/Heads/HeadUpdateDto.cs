namespace OnMed.Persistance.Dtos.Heads;

public class HeadUpdateDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string MiddleName { get; set; } = String.Empty;
    public string BirthDay { get; set; } = string.Empty;
    public bool IsMale { get; set; }
    public string Region { get; set; } = String.Empty;
}
