﻿using OnMed.Application.Exceptions;
using OnMed.Application.Exceptions.Doctors;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Doctors;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.DataAccess.ViewModels.Appoinments;
using OnMed.DataAccess.ViewModels.Users;
using OnMed.Domain.Entities.Doctors;
using OnMed.Domain.Enums;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Appointments;
using OnMed.Service.Interfaces.Auth;
using OnMed.Service.Interfaces.Users;

namespace OnMed.Service.Services.Users;

public class UserAppointmentService : IUserAppointmentService
{
    private readonly IDoctorAppointmentRepository _doctorAppointmentRepository;
    private readonly IHospitalScheduleRepository _hospitalScheduleRepository;
    private readonly IIdentityService _identityService;
    private readonly IDoctorRepository _doctorRepository;

    public UserAppointmentService(IDoctorAppointmentRepository doctorAppointmentRepository,
        IHospitalScheduleRepository hospitalScheduleRepository,
        IIdentityService identityService,
        IDoctorRepository doctorRepository)
    {
        this._doctorAppointmentRepository = doctorAppointmentRepository;
        this._hospitalScheduleRepository = hospitalScheduleRepository;
        this._identityService = identityService;
        this._doctorRepository = doctorRepository;
    }

    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<long> CountByHospitalIdAsync(long hospitalBranchId)
    {
        var result = await _doctorAppointmentRepository.CountByHospitalIdAsync(hospitalBranchId);
        return result;
    }

    public async Task<bool> CreateAsync(AppointmentCreateDto dto)
    {
        var date = DateTime.Parse(dto.RegisterDate);

        DateOnly registerDate = new DateOnly(date.Year, date.Month, date.Day);

        WeekDay dayOfWeek = (WeekDay)((int)registerDate.DayOfWeek);
        var enumResult = Enum.GetName(typeof(WeekDay), dayOfWeek);
        string dayOfWeekString = enumResult is null ? "" : enumResult;

        var dateTime = DateTime.Parse(dto.StartTime);
        var startTime = TimeOnly.FromDateTime(dateTime);


        var hospitalSchedule = await _hospitalScheduleRepository.GetByWeekdayDoctorScheduleAsync(dto.DoctorId, dayOfWeekString, startTime);
        if (hospitalSchedule > 0)
        {
            //bu vaqtga appointment yozish mumkin
            var appointmentFreeTime = await _doctorAppointmentRepository.GetEmptyAppointmentAsync(dto.DoctorId, registerDate, startTime);
            if (appointmentFreeTime == 0)
            {
                var dAppointment = new DoctorAppointment();
                dAppointment.UserId = _identityService.UserId;
                dAppointment.DoctorId = dto.DoctorId;
                dAppointment.Status = AppointmentStatus.Pending;
                dAppointment.HospitalBranchId = dto.HospitalBranchId;
                dAppointment.RegisterDate = registerDate;
                dAppointment.StartTime = startTime;
                dAppointment.DurationMinutes = 30;
                dAppointment.IsPaid = false;
                var freeTime = await _doctorRepository.GetByIdAsync(dto.DoctorId);
                if (freeTime is null) throw new DoctorNotFoundException();
                dAppointment.PaidMoney = freeTime.AppointmentMoney;
                dAppointment.CreatedAt = dAppointment.UpdatedAt = TimeHelper.GetDateTime();
                var result = await _doctorAppointmentRepository.CreateAsync(dAppointment);
                return result > 0;
            }
            else if (appointmentFreeTime > 0) throw new DoctorAlreadyAppoinmentException();
            else if (appointmentFreeTime == -1) throw new InternalServerErrorException();
            else return false;

        }
        else if (hospitalSchedule == 0) throw new NotWorkingTimeException();
        else if (hospitalSchedule == -1) throw new InternalServerErrorException();
        else return false;

    }

    public Task<IList<UserAppointmentViewModel>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<AppointmentViewModel>> GetAllByMomentAsync(int moment)
    {
        long adminId = _identityService.UserId;
        if (moment == 1)
        {
            var result = await _doctorAppointmentRepository.GetAllAppointmentAsync(adminId);
            return result;
        }
        else if (moment == 2)
        {
            var result = await _doctorAppointmentRepository.GetAllByDayAsync(adminId);
            return result;
        }
        else if (moment == 3)
        {
            var result = await _doctorAppointmentRepository.GetAllByWeekAsync(adminId);
            return result;
        }
        else if (moment == 4)
        {
            var result = await _doctorAppointmentRepository.GetAllByMonthAsync(adminId);
            return result;
        }
        else throw new NotFoundException();
    }

    public async Task<IList<UserAppointmentViewModel>> GetByDateAndDoctorIdAsync(long doctorId, DateOnly date)
    {
        var result = await _doctorAppointmentRepository.GetByDateAndDoctorIdAsync(doctorId, date);

        return result;
    }

    public async Task<IList<AppointmentViewModel>> SearchAsync(long branchId, string search)
    {
        var searches = await _doctorAppointmentRepository.SearchAsync(branchId, search);
        return searches;
    }
}
