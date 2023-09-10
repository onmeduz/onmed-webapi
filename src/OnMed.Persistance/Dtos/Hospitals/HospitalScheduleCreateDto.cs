using OnMed.Domain.Enums;

namespace OnMed.Persistance.Dtos.Hospitals;

public class HospitalScheduleCreateDto
{
    public long DoctorId { get; set; }
    public long HospitalBranchId { get; set; }
    public IList<WeekDay> WeekDay { get; set; } = new List<WeekDay>();
    public IList<TimeOnly> StartTime { get; set; }
    public IList<TimeOnly> EndTime { get; set; }
}
