using Onmed.Domain.Enums;
using OnMed.Application.Exceptions;
using OnMed.Application.Exceptions.Categories;
using OnMed.Application.Exceptions.Doctors;
using OnMed.Application.Exceptions.Files;
using OnMed.Application.Exceptions.Hospitals;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Doctors;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.DataAccess.ViewModels.Users;
using OnMed.Domain.Entities.Categories;
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

    public async Task<bool> CreateAsync(AppointmentCreateDto dto)
    {
        var date = dto.RegisterDate;
        DateOnly registerDate = new DateOnly(date.Year,date.Month,date.Day);

        WeekDay dayOfWeek = (WeekDay)((int)registerDate.DayOfWeek);
        string dayOfWeekString = Enum.GetName(typeof(WeekDay), dayOfWeek);

        var doctor = await _hospitalScheduleRepository.GetByDoctorIdAsync(dto.DoctorId);
        if (doctor == null) throw new DoctorNotFoundException();

        var hospitalBranch = await _hospitalScheduleRepository.GetByHospitalBranchIdAsync(dto.HospitalBranchId);
        if(hospitalBranch == null) throw new HospitalBranchNotFoundException();

        var hospitalSchedule = await _hospitalScheduleRepository.GetByWeekdayDoctorScheduleAsync(dto.DoctorId,dayOfWeekString,dto.StartTime);
        if (hospitalSchedule > 0)
        {
            //bu vaqtga appointment yozish mumkin
            var appointmentFreeTime = await _doctorAppointmentRepository.GetEmptyAppointmentAsync(dto.DoctorId, dto.RegisterDate, dto.StartTime);
            if (appointmentFreeTime == 0)
            {
                var dAppointment = new DoctorAppointment();
                dAppointment.UserId = _identityService.UserId;
                dAppointment.DoctorId = dto.DoctorId;
                dAppointment.Status = AppointmentStatus.Pending;
                dAppointment.HospitalBranchId = dto.HospitalBranchId;
                dAppointment.RegisterDate = dto.RegisterDate;
                dAppointment.StartTime = dto.StartTime;
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

    public async Task<IList<UserAppointmentViewModel>> GetByDateAndDoctorIdAsync(long doctorId, DateOnly date)
    {
        if(date == null) date = DateOnly.FromDateTime(DateTime.Now);
        var result = await _doctorAppointmentRepository.GetByDateAndDoctorIdAsync(doctorId, date);

        return result;
    }
}
