namespace OnMed.Domain.Entities.Hospitals;

public class HospitalBranchDoctors : Auditable
{
    public long HospitalBranchId { get; set; }
    
    public long DoctorId { get; set; }
    
    public bool IsActive { get; set; }
    
    public DateTime RegisteredAt { get; set; }
    
    public DateTime StoppedAt { get; set; }
}
