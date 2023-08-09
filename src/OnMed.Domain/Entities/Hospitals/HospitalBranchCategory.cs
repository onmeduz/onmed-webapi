namespace OnMed.Domain.Entities.Hospitals;

public class HospitalBranchCategory : Auditable
{
    public long HospitalBranchId { get; set; }
    public long CategoryId { get; set; }
}
