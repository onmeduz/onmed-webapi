using OnMed.Persistance.Dtos.Auth;

namespace OnMed.Service.Interfaces.Auth;

public interface IAdminAuthService
{
    public Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto);
}
