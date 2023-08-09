namespace OnMed.Domain.Entities.Doctors;

public class DoctorAppointment : Auditable
{
    public long UserId { get; set; }
    public long DoctorId { get; set; }
    public string Status { get; set; } = string.Empty;
    public long HospitalBranchId { get; set; }
    public DateOnly RegisterDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public int DurationMinutes { get; set; }
    public string PaymentType { get; set; } = string.Empty;
    public string PaymentProvider { get; set; } = string.Empty;
    public bool IsPaid { get; set; }
    public string Description { get; set; } = string.Empty;
    public double PaidMoney { get; set; }
    public string PaymentDescription { get; set; } = string.Empty;
    public byte Stars { get; set; }
}
