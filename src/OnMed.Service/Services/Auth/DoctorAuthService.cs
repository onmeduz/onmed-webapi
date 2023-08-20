using OnMed.Application.Exceptions.Auth;
using OnMed.Application.Exceptions.Doctors;
using OnMed.DataAccess.Interfaces.Doctors;
using OnMed.Service.Common.Security;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Service.Interfaces.Auth;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Notifications;
using OnMed.Persistance.Dtos.Security;
using Microsoft.Extensions.Caching.Memory;
using OnMed.Service.Interfaces.Notifications;

namespace OnMed.Service.Services.Auth;

public class DoctorAuthService : IDoctorAuthService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ITokenService _tokenService;
    private readonly ISmsSender _smsSender;
    private readonly IMemoryCache _memoryCache;
    private const string VERIFY_RESET_CACHE_KEY = "verify_reset_";
    private const int VERIFICATION_MAXIMUM_ATTEMPTS = 3;
    private const int CACHED_MINUTES_FOR_VERIFICATION = 5;

    public DoctorAuthService(IDoctorRepository doctorRepository,
        ITokenService tokenService,
        ISmsSender smsSender,
        IMemoryCache memoryCache)
    {
        this._doctorRepository = doctorRepository;
        this._tokenService = tokenService;
        this._memoryCache = memoryCache;
        this._smsSender = smsSender;
    }

    public async Task<(bool Result, string Token)> LoginAsync(LoginDto loginDto)
    {
        var doctor = await _doctorRepository.GetByPhoneNumberAsync(loginDto.PhoneNumber);
        if (doctor is null) throw new DoctorNotFoundException();

        var hasherResult = PasswordHasher.Verify(loginDto.Password, doctor.PasswordHash, doctor.Salt);
        if (hasherResult == false) throw new PasswordNotMatchException();

        string token = _tokenService.GenerateToken(doctor);
        return (Result: true, Token: token);
    }
#pragma warning disable
    public async Task<(bool Result, int CachedVerificationMinutes)> SendCodeForResetPasswordAsync(string phone)
    {
        var doctor = await _doctorRepository.GetByPhoneNumberAsync(phone);
        if (doctor is null) throw new DoctorNotFoundException();
        VerificationDto verificationDto = new VerificationDto();
        verificationDto.Attempt = 0;
        verificationDto.CreatedAt = TimeHelper.GetDateTime();
        //verificationDto.Code = CodeGenerator.GenerateRandomNumber();
        verificationDto.Code = 11111;

        if (_memoryCache.TryGetValue(VERIFY_RESET_CACHE_KEY + phone, out VerificationDto oldVerificationDto))
        {
            _memoryCache.Remove(VERIFY_RESET_CACHE_KEY + phone);
        }

        _memoryCache.Set(VERIFY_RESET_CACHE_KEY + phone, verificationDto,
            TimeSpan.FromMinutes(CACHED_MINUTES_FOR_VERIFICATION));

        SmsMessage smsMessage = new SmsMessage();
        smsMessage.Title = "On Med";
        smsMessage.Content = "Sizning tasdiqlash kodingiz : " + verificationDto.Code;
        smsMessage.Recipent = phone.Substring(1);
        var smsResult = await _smsSender.SendAsync(smsMessage);
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
                var doctor = await _doctorRepository.GetByPhoneNumberAsync(phone);
                if (doctor is null) throw new DoctorNotFoundException();
                string token = _tokenService.GenerateToken(doctor);

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

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
    {
        var doctor = await _doctorRepository.GetByPhoneNumberAsync(dto.PhoneNumber);
        if (doctor is null) throw new DoctorNotFoundException();
        var hasherResult = PasswordHasher.Hash(dto.Password);
        doctor.PasswordHash = hasherResult.Hash;

        doctor.Salt = hasherResult.Salt;
        doctor.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _doctorRepository.UpdateAsync(doctor.Id, doctor);

        return result > 0;
    }
}
