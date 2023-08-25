using OnMed.DataAccess.ViewModels.Appoinments;
using OnMed.DataAccess.ViewModels.Users;
using OnMed.Domain.Entities.Doctors;

namespace OnMed.DataAccess.Interfaces.Doctors;

public interface IDoctorAppointmentRepository : IRepository<DoctorAppointment>
{
    public Task<int> GetEmptyAppointmentAsync(long doctorId, DateOnly registerDate, TimeOnly startTime);
    public Task<IList<UserAppointmentViewModel>> GetByDateAndDoctorIdAsync(long doctorId, DateOnly date);
    public Task<long> CountByHospitalIdAsync(long hospitalIBranchd);
    public Task<IList<AppointmentViewModel>> GetAllByDayAsync(long adminId);
    public Task<IList<AppointmentViewModel>> GetAllByWeekAsync(long adminId);
    public Task<IList<AppointmentViewModel>> GetAllByMonthAsync(long adminId);
    public Task<IList<AppointmentViewModel>> GetAllAppointmentAsync(long adminId);

}
