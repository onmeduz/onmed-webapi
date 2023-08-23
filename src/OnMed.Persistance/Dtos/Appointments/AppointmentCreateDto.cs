using Onmed.Domain.Enums;

namespace OnMed.Persistance.Dtos.Appointments;

public class AppointmentCreateDto
{
    public long DoctorId { get; set; }
    public long HospitalBranchId { get; set; }
    public DateOnly RegisterDate { get; set; }
    public TimeOnly StartTime { get; set; }
}
