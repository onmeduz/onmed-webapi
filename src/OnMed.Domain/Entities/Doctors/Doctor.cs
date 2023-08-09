namespace OnMed.Domain.Entities.Doctors;

public class Doctor : Human
{
    public double AppointmentMoney { get; set; }
    public string Degree { get; set; } = string.Empty;
}
