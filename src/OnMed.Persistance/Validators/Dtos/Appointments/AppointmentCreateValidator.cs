using FluentValidation;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Appointments;

namespace OnMed.Persistance.Validators.Dtos.Appointments;

public class AppointmentCreateValidator: AbstractValidator<AppointmentCreateDto>
{
    public AppointmentCreateValidator()
    {
        RuleFor(dto => dto.DoctorId).NotNull().NotEmpty().WithMessage("DoctorId field is required!")
            .GreaterThanOrEqualTo(0).WithMessage("is field not minimum 0");
        RuleFor(dto => dto.HospitalBranchId).NotNull().NotEmpty().WithMessage("HospitalBranchId field is required!")
            .GreaterThanOrEqualTo(0).WithMessage("is field not minimum 0");
        RuleFor(dto => dto.RegisterDate).NotNull().NotEmpty().WithMessage("RegisterDate field is required!");
        RuleFor(dto => dto.StartTime).NotNull().NotEmpty().WithMessage("StartTime field is required!");
    }
}
