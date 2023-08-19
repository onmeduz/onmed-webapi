using OnMed.Application.Exceptions.Auth;
using OnMed.Application.Exceptions.Doctors;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces;
using OnMed.DataAccess.Interfaces.Doctors;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.Domain.Entities.Categories;
using OnMed.Domain.Entities.Doctors;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Domain.Entities.Users;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Dtos.Doctors;
using OnMed.Service.Common.Security;
using OnMed.Service.Interfaces.Auth;
using OnMed.Service.Interfaces.Common;
using OnMed.Service.Interfaces.Doctors;

namespace OnMed.Service.Services.Doctors;

public class DoctorService : IDoctorService
{
    private readonly IFileService _fileService;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IHospitalBranchDoctorRepository _branchDoctorRepository;

    public DoctorService(IFileService fileService,
        IDoctorRepository doctorRepository,
        IHospitalBranchDoctorRepository branchDoctorRepository)
    {
        this._fileService = fileService;
        this._doctorRepository = doctorRepository;
        this._branchDoctorRepository = branchDoctorRepository;
    }
    public async Task<long> CountByHospitalAsync(long hospitalId)
    {
        var result =  await _branchDoctorRepository.CountAsync();

        return result;
    }

    public async Task<bool> CreateAsync(DoctorCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.Image, "doctors");

        Doctor doctor = new Doctor()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            BirthDay = dto.BirthDay,
            PhoneNumber = dto.PhoneNumber,
            IsMale = dto.IsMale,
            Region = dto.Region,
            AppointmentMoney = dto.AppointmentMoney,
            Degree = dto.Degree,
            ImagePath = imagepath,
            CreatedAt = TimeHelper.GetDateTime(),
            UpdatedAt = TimeHelper.GetDateTime()
        };

        var hasherResult = PasswordHasher.Hash(dto.Password);
        doctor.PasswordHash = hasherResult.Hash;
        doctor.Salt = hasherResult.Salt;
        var result = await _doctorRepository.CreateReturnIdAsync(doctor);

        if (result > 0)
        {
            HospitalBranchDoctor branchdoctor = new HospitalBranchDoctor();
            branchdoctor.HospitalBranchId = dto.HospitalId;
            branchdoctor.DoctorId = result;
            branchdoctor.IsActive = true;
            branchdoctor.RegisteredAt = dto.RegisteredAt;
            branchdoctor.CreatedAt = branchdoctor.UpdatedAt = TimeHelper.GetDateTime();
            var dbResult = await _branchDoctorRepository.CreateAsync(branchdoctor);
            result = dbResult;
        }

        return result > 0;
    }

    public Task<bool> DeleteAsync(long doctorId)
    {
        throw new NotImplementedException();
    }

    public Task<IList<Category>> GetAllByHospitalAsync(long hospitalId, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<Category> GetByIdAsync(long doctorId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(long doctorId, DoctorUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
