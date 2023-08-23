using OnMed.Domain.Entities.Doctors;

namespace OnMed.DataAccess.Interfaces.Doctors;

public interface IDoctorAppointmentRepository : IRepository<DoctorAppointment>
{
    public Task<int> GetEmptyAppointmentAsync(long doctorId, DateOnly registerDate, TimeOnly startTime);
}
