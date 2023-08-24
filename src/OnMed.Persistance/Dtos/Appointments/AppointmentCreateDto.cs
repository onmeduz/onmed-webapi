namespace OnMed.Persistance.Dtos.Appointments;

public class AppointmentCreateDto
{
    public long DoctorId { get; set; }
    public long HospitalBranchId { get; set; }
    public string RegisterDate { get; set; } = string.Empty;
    public string StartTime { get; set; } = string.Empty;
}
