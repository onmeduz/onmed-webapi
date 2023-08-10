using OnMed.Domain.Entities.Administrators;
using OnMed.Domain.Entities.Doctors;
using OnMed.Domain.Entities.Heads;
using OnMed.Domain.Entities.Users;

namespace OnMed.Service.Interfaces.Auth;

public interface ITokenService
{
    public string GenerateToken(User user);
    public string GenerateToken(Administrator admin);
    public string GenerateToken(Doctor doctor);
    public string GenerateToken(Head head);
}
