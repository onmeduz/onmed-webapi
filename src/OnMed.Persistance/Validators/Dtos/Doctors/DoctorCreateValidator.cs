using FluentValidation;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Doctors;

namespace OnMed.Persistance.Validators.Dtos.Doctors;

public class DoctorCreateValidator : AbstractValidator<DoctorCreateDto>
{
    public DoctorCreateValidator()
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

        RuleFor(dto => dto.IsMale).NotNull().NotEmpty().WithMessage("IsMale field is required!");

        int maxImageSizeMB = 5;
        RuleFor(dto => dto.Image).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.Image.Length).LessThan(maxImageSizeMB * 1024 * 1024)
            .WithMessage($"Image size must be less than {maxImageSizeMB} MB");
        
        RuleFor(dto => dto.Image.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");

        RuleFor(dto => dto.Region).NotNull().NotEmpty().WithMessage("Region field is required!");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
            .WithMessage("Password is not strong password!");

        RuleFor(dto => dto.AppointmentMoney).NotNull().NotEmpty().WithMessage("AppointmentMoney field is required!");

        RuleFor(dto => dto.Degree).NotNull().NotEmpty().WithMessage("Degree field is required!");
       
        RuleFor(dto => dto.HospitalBranchId).NotNull().NotEmpty().WithMessage("Degree field is required!");
    }
}
