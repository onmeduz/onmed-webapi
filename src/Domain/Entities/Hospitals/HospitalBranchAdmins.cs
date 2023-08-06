namespace OnMed.Domain.Entities.Hospitals;

public class HospitalBranchAdmins : Auditable
{
    public long HospitalBranchId { get; set; }

    public long AdministratorId { get; set; }
}