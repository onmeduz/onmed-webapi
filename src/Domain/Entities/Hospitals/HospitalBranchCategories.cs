namespace OnMed.Domain.Entities.Hospitals;

public class HospitalBranchCategories : Auditable
{
    public long HospitalBranchId { get; set; }
    
    public long categoryId { get; set; }
}
