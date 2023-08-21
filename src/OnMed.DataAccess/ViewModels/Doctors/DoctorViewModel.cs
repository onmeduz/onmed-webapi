namespace OnMed.DataAccess.ViewModels.Doctors;

public class DoctorViewModel : HumanViewModel
{
    public string Degree { get; set; } = string.Empty;
    public double AppointmentMoney { get; set; }
    public decimal StarCount { get; set; }
}

