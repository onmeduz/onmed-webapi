namespace OnMed.DataAccess.ViewModels.Doctors;

public class DoctorViewModel : HumanViewModel
{
    public string Degree { get; set; } = string.Empty;
    public double AppointmentMoney { get; set; }
    public decimal StarCount { get; set; }
    public string[] Weekday { get; set;} = {};
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public long HospitalBranchId { get; set; }

}

