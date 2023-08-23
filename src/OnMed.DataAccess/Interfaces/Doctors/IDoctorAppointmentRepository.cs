using OnMed.DataAccess.ViewModels.Users;
using OnMed.Domain.Entities.Doctors;

namespace OnMed.DataAccess.Interfaces.Doctors;

public interface IDoctorAppointmentRepository : IRepository<DoctorAppointment>
{
    public Task<int> GetEmptyAppointmentAsync(long doctorId, DateOnly registerDate, TimeOnly startTime);
    public Task<IList<UserAppointmentViewModel>> GetByDateAndDoctorIdAsync(long doctorId, DateOnly date);
}
