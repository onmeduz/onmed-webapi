using Microsoft.AspNetCore.Http;

namespace OnMed.Persistance.Dtos.Doctors;

public class DoctorCreateDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string MiddleName { get; set; } = String.Empty;
    public DateOnly BirthDay { get; set; }
    public string PhoneNumber { get; set; } = String.Empty;
    public bool IsMale { get; set; }
    public IFormFile Image { get; set; } = default!;
    public string Region { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public double AppointmentMoney { get; set; }
    public string Degree { get; set; } = string.Empty;
    public long HospitalBranchId { get; set; }
    public List<long> CategoryIds { get; set; } = new List<long>();
}
