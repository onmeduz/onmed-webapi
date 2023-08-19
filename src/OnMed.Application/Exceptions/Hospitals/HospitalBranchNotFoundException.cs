namespace OnMed.Application.Exceptions.Hospitals;

public class HospitalBranchNotFoundException : NotFoundException
{
    public HospitalBranchNotFoundException()
    {
        this.TitleMessage = "HospitalBranch not found!";
    }
}
