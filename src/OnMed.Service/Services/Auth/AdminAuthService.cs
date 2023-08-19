using Microsoft.Extensions.Caching.Memory;
using OnMed.Application.Exceptions.Administrators;
using OnMed.Application.Exceptions.Auth;
using OnMed.DataAccess.Interfaces.Administrators;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Dtos.Notifications;
using OnMed.Persistance.Dtos.Security;
using OnMed.Service.Common.Security;
using OnMed.Service.Interfaces.Auth;

namespace OnMed.Service.Services.Auth;

public class AdminAuthService : IAdminAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IAdministratorRepository _adminRepository;
    private readonly IMemoryCache _memoryCache;
    private const string VERIFY_RESET_CACHE_KEY = "verify_reset_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;

    public AdminAuthService(ITokenService tokenService,
        IAdministratorRepository administratorRepository,
        IMemoryCache memoryCache)
    {
        this._tokenService = tokenService;
        this._adminRepository = administratorRepository;
        this._memoryCache = memoryCache;
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

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
    {
        var admin = await _adminRepository.GetByPhoneNumberAsync(dto.PhoneNumber);
        if (admin is null) throw new AdminNotFoundException();
        var hasherResult = PasswordHasher.Hash(dto.Password);
        admin.PasswordHash = hasherResult.Hash;
        admin.Salt = hasherResult.Salt;
        admin.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _adminRepository.UpdateAsync(admin.Id, admin);

        return result > 0;
    }

#pragma warning disable
    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForResetPasswordAsync(string phone)
    {
        var admin = await _adminRepository.GetByPhoneNumberAsync(phone);
        if (admin is null) throw new AdminNotFoundException();
        VerificationDto verificationDto = new VerificationDto();
        verificationDto.Attempt = 0;
        verificationDto.CreatedAt = TimeHelper.GetDateTime();
        //verificationDto.Code = CodeGenerator.GenerateRandomNumber();
        verificationDto.Code = 11111;

        if (_memoryCache.TryGetValue(VERIFY_RESET_CACHE_KEY + phone, out VerificationDto oldVerifcationDto))
        {
            _memoryCache.Remove(VERIFY_RESET_CACHE_KEY + phone);
        }

        _memoryCache.Set(VERIFY_RESET_CACHE_KEY + phone, verificationDto,
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

        SmsMessage smsMessage = new SmsMessage();
        smsMessage.Title = "On Med";
        smsMessage.Content = "Sizning tasdiqlash kodingiz : " + verificationDto.Code;
        smsMessage.Recipent = phone.Substring(1);


        var smsResult = true; //await _smsSender.SendAsync(smsMessage);
        if (smsResult is true) return (Result: true, CachedVerificationMinutes: CACHED_MINUTES_FOR_VERIFICATION);
        else return (Result: false, CachedVerificationMinutes: 0);
    }

    public async Task<(bool Result, string Token)> VerifyResetPasswordAsync(string phone, int code)
    {
        if (_memoryCache.TryGetValue(VERIFY_RESET_CACHE_KEY + phone, out VerificationDto verificationDto))
        {
            if (verificationDto.Attempt >= VERIFICATION_MAXIMUM_ATTEMPTS)
                throw new VerificationTooManyRequestsException();
            else if (verificationDto.Code == code)
            {
                var admin = await _adminRepository.GetByPhoneNumberAsync(phone);
                if (admin is null) throw new AdminNotFoundException();
                string token = _tokenService.GenerateToken(admin);

                return (Result: true, Token: token);
            }
            else
            {
                _memoryCache.Remove(VERIFY_RESET_CACHE_KEY + phone);
                verificationDto.Attempt++;
                _memoryCache.Set(VERIFY_RESET_CACHE_KEY + phone, verificationDto,
                    TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

                return (Result: false, Token: "");
            }
        }
        else throw new VerificationCodeExpiredException();
    }
}
