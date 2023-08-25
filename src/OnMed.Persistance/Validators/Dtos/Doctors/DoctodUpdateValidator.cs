using FluentValidation;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Doctors;

namespace OnMed.Persistance.Validators.Dtos.Doctors;

public class DoctodUpdateValidator : AbstractValidator<DoctorUpdateDto>
{
    public DoctodUpdateValidator()
    {
            RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("FirstName field is required!")
                .MinimumLength(2).WithMessage("FirstName must be more than 2 characters")
                .MaximumLength(20).WithMessage("FirstName must be less than 20 characters");

            RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("LastName field is required!")
                .MinimumLength(2).WithMessage("LastName must be more than 2 characters")
                .MaximumLength(20).WithMessage("LastName must be less than 20 characters");

            RuleFor(dto => dto.MiddleName).NotNull().NotEmpty().WithMessage("MiddleName field is required!")
                .MinimumLength(2).WithMessage("MiddleName must be more than 2 characters")
                .MaximumLength(20).WithMessage("MiddleName must be less than 20 characters");

            RuleFor(dto => dto.BirthDay).NotNull().NotEmpty().WithMessage("Birth day field is required!");

            RuleFor(dto => dto.PhoneNumber).NotNull().NotEmpty().WithMessage("Phone number is required!")
                .Must(phone => PhoneNumberValidator.IsValid(phone)).WithMessage("Phone number is incorrect!");

            RuleFor(dto => dto.IsMale).NotNull().WithMessage("IsMale field is required!");

            RuleFor(dto => dto.Region).NotNull().NotEmpty().WithMessage("Region field is required!");

            RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
                .WithMessage("Password is not strong password!");

            RuleFor(dto => dto.AppointmentMoney).NotNull().NotEmpty().WithMessage("AppointmentMoney field is required!");

            RuleFor(dto => dto.Degree).NotNull().NotEmpty().WithMessage("Degree field is required!");
    }
}
