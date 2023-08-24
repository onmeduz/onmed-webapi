using OnMed.Domain.Entities.Hospitals;

namespace OnMed.DataAccess.Interfaces.Hospitals;

public interface IHospitalScheduleRepository : IRepository<HospitalSchedule>
{
    public Task<HospitalSchedule?> GetByDoctorIdAsync(long doctorId);
    public Task<HospitalSchedule?> GetByHospitalBranchIdAsync(long hospitalId);
    public Task<int> GetByWeekdayDoctorScheduleAsync(long doctorId, string weekday, TimeOnly time);
}
