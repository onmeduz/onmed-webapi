namespace OnMed.Application.Exceptions.Doctors;

public class DoctorNotFoundException : NotFoundException
{
    public DoctorNotFoundException()
    {
        TitleMessage = "Doctor not found";
    }
}
