namespace OnMed.Application.Exceptions.Doctors;

public class DoctorAlreadyAppoinmentException : AlreadyAppointmentException
{
    public DoctorAlreadyAppoinmentException()
    {
        this.TitleMessage = "Kechirasiz bu vaqt oldin band qilingan!";
    }
}
