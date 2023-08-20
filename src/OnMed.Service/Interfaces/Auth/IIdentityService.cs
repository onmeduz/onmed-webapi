using Microsoft.AspNetCore.Identity;

namespace OnMed.Service.Interfaces.Auth;

public interface IIdentityService
{
    public long UserId { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string MiddleName { get; }
    public string PhoneNumber { get; }
    public string? IdentityRole { get; }
}
