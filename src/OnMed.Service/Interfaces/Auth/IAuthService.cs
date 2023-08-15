using OnMed.Persistance.Dtos.Auth;

namespace OnMed.Service.Interfaces.Auth;

public interface IAuthService
{
    public Task<(bool Result, int CachedMinutes)> RegisterAsync(RegisterDto dto);
    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForRegisterAsync(string phone);
    public Task<(bool Result, string Token)> VerifyRegisterAsync(string phone, int code);
    public Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto);
    public Task<(bool Result, int CachedVerificationMinutes)> SendCodeForResetPasswordAsync(string phone);
    public Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phone, int code);
    public Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
}
