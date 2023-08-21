using OnMed.Application.Exceptions;
using OnMed.Application.Exceptions.Administrators;
using OnMed.Application.Exceptions.Auth;
using OnMed.DataAccess.Interfaces.Heads;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Service.Common.Security;
using OnMed.Service.Interfaces.Auth;

namespace OnMed.Service.Services.Auth;

public class HeadAuthService : IHeadAuthService
{
    private readonly IHeadRepository _headRepository;
    private readonly ITokenService _tokenService;

    public HeadAuthService(IHeadRepository headRepository,
        ITokenService tokenService)
    {
        this._headRepository = headRepository;
        this._tokenService = tokenService;
    }

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var head = await _headRepository.GetByPhoneNumberAsync(loginDto.PhoneNumber);
        if (head is null) throw new HeadNotFoundException();
        var hasherResult = PasswordHasher.Verify(loginDto.Password, head.PasswordHash, head.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = _tokenService.GenerateToken(head);
        return (Result: true, Token: token);
    }
}
