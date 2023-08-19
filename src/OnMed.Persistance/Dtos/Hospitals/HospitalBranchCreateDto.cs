using Microsoft.AspNetCore.Http;

namespace OnMed.Persistance.Dtos.Hospitals;

public class HospitalBranchCreateDto
{
    public string Name { get; set; } = string.Empty;
    public long HospitalId { get; set; }
    public IFormFile Image { get; set; } = default!;
    public string Region { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public double AdressLatitude { get; set; }
    public double AdressLongitude { get; set; }
    public string ContactPhoneNumber { get; set; } = string.Empty;
}
