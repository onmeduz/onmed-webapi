using OnMed.Persistance.Dtos.Auth;

namespace OnMed.Service.Interfaces.Doctors;

public interface IDoctorService
{
    public Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto);
}
