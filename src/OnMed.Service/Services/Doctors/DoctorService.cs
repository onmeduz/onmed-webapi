using OnMed.Application.Exceptions.Auth;
using OnMed.Application.Exceptions.Doctors;
using OnMed.DataAccess.Interfaces.Doctors;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Service.Common.Security;
using OnMed.Service.Interfaces.Auth;
using OnMed.Service.Interfaces.Doctors;

namespace OnMed.Service.Services.Doctors;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ITokenService _tokenService;

    public DoctorService(IDoctorRepository doctorRepository,
        ITokenService tokenService)
    {
        this._doctorRepository = doctorRepository;
        this._tokenService = tokenService;
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
}
