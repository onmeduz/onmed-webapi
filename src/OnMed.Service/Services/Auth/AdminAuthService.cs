using OnMed.Application.Exceptions.Administrators;
using OnMed.Application.Exceptions.Auth;
using OnMed.DataAccess.Interfaces.Administrators;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Service.Common.Security;
using OnMed.Service.Interfaces.Auth;

namespace OnMed.Service.Services.Auth;

public class AdminAuthService : IAdminAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IAdministratorRepository _adminRepository;

    public AdminAuthService(ITokenService tokenService,
        IAdministratorRepository administratorRepository)
    {
        this._tokenService = tokenService;
        this._adminRepository = administratorRepository;
    }

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var admin = await _adminRepository.GetByPhoneNumberAsync(loginDto.PhoneNumber);
        if (admin is null) throw new AdminNotFoundException();
        var hasherResult = PasswordHasher.Verify(loginDto.Password, admin.PasswordHash, admin.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = _tokenService.GenerateToken(admin);
        return (Result: true, Token: token);
    }
}
