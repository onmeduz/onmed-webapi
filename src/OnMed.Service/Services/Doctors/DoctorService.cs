﻿using Onmed.Domain.Entities.Doctors;
using OnMed.Application.Exceptions.Doctors;
using OnMed.Application.Exceptions.Users;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Doctors;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.DataAccess.ViewModels.Doctors;
using OnMed.Domain.Entities.Doctors;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Doctors;
using OnMed.Service.Common.Security;
using OnMed.Service.Interfaces.Common;
using OnMed.Service.Interfaces.Doctors;

namespace OnMed.Service.Services.Doctors;

public class DoctorService : IDoctorService
{
    private readonly IFileService _fileService;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IHospitalBranchDoctorRepository _branchDoctorRepository;
    private readonly IPaginator _paginator;
    private readonly IHospitalScheduleRepository _hospitalSchedule;
    private readonly IDoctorCategoryRepository _doctorCategoryRepository;

    public DoctorService(IFileService fileService,
        IDoctorRepository doctorRepository,
        IHospitalBranchDoctorRepository branchDoctorRepository,
        IPaginator paginator,
        IHospitalScheduleRepository hospitalSchedule,
        IDoctorCategoryRepository doctorCategoryRepository)
    {
        this._fileService = fileService;
        this._doctorRepository = doctorRepository;
        this._branchDoctorRepository = branchDoctorRepository;
        this._paginator = paginator;
        this._hospitalSchedule = hospitalSchedule;
        this._doctorCategoryRepository = doctorCategoryRepository;
    }
    public async Task<long> CountByHospitalAsync(long hospitalId)
    {
        var result = await _branchDoctorRepository.CountByHospitalAsync(hospitalId);

        return result;
    }

    public async Task<long> CountAsync() => await _doctorRepository.CountAsync();


    public async Task<bool> CreateAsync(DoctorCreateDto dto)
    {
        var doctorPhone = await _doctorRepository.GetByPhoneNumberAsync(dto.PhoneNumber);
        if (doctorPhone is not null) throw new UserAlreadyExistsException(dto.PhoneNumber);

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
        var doctorId = await _doctorRepository.CreateReturnIdAsync(doctor);
        bool res = false;

        if (doctorId > 0)
        {
            HospitalBranchDoctor branchdoctor = new HospitalBranchDoctor();
            branchdoctor.HospitalBranchId = dto.HospitalBranchId;
            branchdoctor.DoctorId = doctorId;
            branchdoctor.IsActive = true;
            branchdoctor.RegisteredAt = TimeHelper.GetDateTime();
            branchdoctor.CreatedAt = branchdoctor.UpdatedAt = TimeHelper.GetDateTime();
            var dbResult = await _branchDoctorRepository.CreateAsync(branchdoctor);
            res = dbResult > 0;
            if (res)
            {
                // create doctor_schedule!

                var hospitalSchedule = new HospitalSchedule();
                hospitalSchedule.DoctorId = doctorId;
                hospitalSchedule.HospitalBranchId = dto.HospitalBranchId;
                for (int i = 0; i < dto.WeekDay.Count; i++)
                {
                    hospitalSchedule.Weekday[i] = dto.WeekDay[i].ToString();
                }
                hospitalSchedule.StartTime = dto.StartTime;
                hospitalSchedule.EndTime = dto.EndTime;
                hospitalSchedule.CreatedAt = hospitalSchedule.UpdatedAt = TimeHelper.GetDateTime();
                res = await _hospitalSchedule.CreateAsync(hospitalSchedule) > 0;

                if (res)
                {
                    int count = 0;
                    foreach (var categoryid in dto.CategoryIds)
                    {
                        var doctorCategory = new DoctorCategory();
                        doctorCategory.DoctorId = doctorId;
                        doctorCategory.CategoryId = categoryid;
                        doctorCategory.CreatedAt = doctorCategory.UpdatedAt = TimeHelper.GetDateTime();

                        var result = await _doctorCategoryRepository.CreateAsync(doctorCategory);
                        count += result;
                    }

                    if (count == dto.CategoryIds.Count) return true;
                    return false;
                }
            }
        }

        return res;
    }

    public async Task<bool> DeleteAsync(long doctorId)
    {
        return await _doctorRepository.DeleteAsync(doctorId) > 0;
    }

    public async Task<IList<DoctorViewModel>> GetAllAsync(PaginationParams @params)
    {
        var doctors = await _doctorRepository.GetAllAsync(@params);
        var count = await _doctorRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return doctors;
    }

    public async Task<IList<DoctorViewModel>> GetAllByHospitalAsync(long hospitalId, long? categoryId, PaginationParams @params)
    {
        if (categoryId == null)
        {
            var doctors = await _doctorRepository.GetAllHospitalIdAsync(hospitalId, @params);
            var count = await _branchDoctorRepository.CountByHospitalAsync(hospitalId);
            _paginator.Paginate(count, @params);
            return doctors;
        }
        else
        {
            var doctors = await _doctorRepository.GetAllHospitalIdAndCategoryIdAsync(hospitalId, categoryId, @params);
            var count = await _branchDoctorRepository.CountByHospitalAsync(hospitalId);
            _paginator.Paginate(count, @params);
            return doctors;
        }

    }

    public async Task<DoctorViewModel> GetByIdAsync(long doctorId)
    {
        var doctor = await _doctorRepository.GetByIdViewAsync(doctorId);
        if (doctor is null) throw new DoctorNotFoundException();
        else return doctor;
    }

    public async Task<bool> UpdateAsync(long Id, DoctorUpdateDto dto)
    {
        var doctorPhone = await _doctorRepository.GetByIdAsync(Id);
        if (doctorPhone is null) throw new DoctorNotFoundException();

        string imagepath = "";
        if (dto.Image is not null)
        {
            imagepath = await _fileService.UploadImageAsync(dto.Image, "doctors");
        }
        else
        {
            imagepath = doctorPhone.ImagePath;
        }
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
        var doctorId = await _doctorRepository.UpdateAsync(Id, doctor);

        return doctorId > 0;
    }

    public async Task<IList<DoctorViewModel>> SearchAsync(string search)
    {
        var searches = await _doctorRepository.SearchAsync(search);
        return searches;
    }

    public async Task<IList<DoctorViewModel>> SearchAsync(long branchId, string search)
    {
        var searches = await _doctorRepository.SearchAsync(branchId, search);
        return searches;
    }
}
