using FluentValidation;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Hospitals;

namespace OnMed.Persistance.Validators.Dtos.Hospitals;

public class HospitalCreateValidator : AbstractValidator<HospitalCreateDto>
{
    public HospitalCreateValidator()
    {
        RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters");

        RuleFor(dto => dto.LegalName).NotNull().NotEmpty().WithMessage("LegalName field is required!")
            .MinimumLength(3).WithMessage("LegalName must be more than 3 characters")
            .MaximumLength(50).WithMessage("LegalName must be less than 50 characters");

        int maxImageSizeMB = 5;
        RuleFor(dto => dto.BrandImage).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.BrandImage.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1)
            .WithMessage($"Image size must be less than {maxImageSizeMB} MB");

        RuleFor(dto => dto.BrandImage.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");

        RuleFor(dto => dto.AdministratorPhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.FaxPhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Email).Must(email => EmailValidator.IsValidEmail(email))
            .WithMessage("Email is valid! ex: email@gmail.com");

        RuleFor(dto => dto.LicenseNumber).NotNull().NotEmpty().WithMessage("LicenseNumber field is required!")
            .MinimumLength(3).WithMessage("LicenseNumber must be more than 3 characters")
            .MaximumLength(50).WithMessage("LicenseNumber must be less than 50 characters");
       

    }
}
