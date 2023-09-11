using OnMed.Domain.Enums;

namespace OnMed.DataAccess.ViewModels.Appoinments;

public class AppointmentViewModel
{
    public long AppointmentId { get; set; }
    public long UserId { get; set; }
    public long DoctorId { get; set; }
    public long HospitalBranchId { get; set; }
    public string UserFullname { get; set; } = string.Empty;
    public string UserPhoneNumber { get; set; } = string.Empty;
    public bool UserIsMale { get; set; }
    public string UserImagePath { get; set; } = string.Empty;
    public string DoctorFullname { get; set; } = string.Empty;
    public string doctorPhoneNumber { get; set; } = string.Empty;
    public bool DoctorIsMale { get; set; }
    public string Degree { get; set; } = string.Empty;
    public AppointmentStatus Status { get; set; }
    public DateOnly RegisterDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public int DurationMinutes { get; set; }
    public bool IsPaid { get; set; }
    public double PaidMoney { get; set; }
}
