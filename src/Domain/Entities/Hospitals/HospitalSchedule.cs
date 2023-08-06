namespace OnMed.Domain.Entities.Hospitals;

public class HospitalSchedule : Auditable
{
    public long HospitalBranchId { get; set; }
    
    public long DoctorId { get; set; }
    
    public string Weekday { get; set; } = string.Empty;
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly End_Time { get; set; }
    
    public string description { get; set; } = string.Empty;
}
