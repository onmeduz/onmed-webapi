namespace OnMed.Domain.Entities.Hospitals;

public class HospitalBranchAdmin : Auditable
{
    public long HospitalBranchId { get; set; }
    public long AdministratorsId { get; set; }
}