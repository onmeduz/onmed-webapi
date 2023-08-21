namespace OnMed.Domain.Entities.Hospitals;

public class HospitalSchedule : Auditable
{
    public long HospitalBranchId { get; set; }
    public long DoctorId { get; set; }
    public string Weekday { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Description { get; set; } = string.Empty;
}
