namespace OnMed.Application.Exceptions.Hospitals;

public class HospitalNotFoundException : NotFoundException
{
    public HospitalNotFoundException()
    {
        this.TitleMessage = "Hospital not found!";
    }
}
