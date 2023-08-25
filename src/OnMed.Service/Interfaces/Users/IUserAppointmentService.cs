using OnMed.Application.Utils;
using OnMed.DataAccess.ViewModels.Appoinments;
using OnMed.DataAccess.ViewModels.Users;
using OnMed.Persistance.Dtos.Appointments;

namespace OnMed.Service.Interfaces.Users;

public interface IUserAppointmentService
{
    public Task<IList<UserAppointmentViewModel>> GetAllAsync(PaginationParams @params);
    public Task<IList<UserAppointmentViewModel>> GetByDateAndDoctorIdAsync(long doctorId, DateOnly date);
    public Task<bool> CreateAsync(AppointmentCreateDto dto);
    public Task<long> CountAsync();
    public Task<long> CountByHospitalIdAsync(long hospitalBranchId);
    public Task<IList<AppointmentViewModel>> GetAllByMomentAsync(int moment);

}
