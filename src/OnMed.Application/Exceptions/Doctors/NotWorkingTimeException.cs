namespace OnMed.Application.Exceptions.Doctors;

public class NotWorkingTimeException : AlreadyAppointmentException
{
    public NotWorkingTimeException()
    {
        this.TitleMessage = "Kechirasiz bu vaqt shifokorning ish vaqti emas !";
    }
}
