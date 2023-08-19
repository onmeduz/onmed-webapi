using FluentValidation;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Hospitals;

namespace OnMed.Persistance.Validators.Dtos.Hospitals;

public class HospitalBranchUpdateValidator : AbstractValidator<HospitalBranchUpdateDto>
{
    public HospitalBranchUpdateValidator()
    {
        RuleFor(dto => dto.Name).NotNull().NotEmpty().WithMessage("Name field is required!")
            .MinimumLength(3).WithMessage("Name must be more than 3 characters")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters");

        int maxImageSizeMB = 5;
        RuleFor(dto => dto.Image).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.Image.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1)
            .WithMessage($"Image size must be less than {maxImageSizeMB} MB");

        RuleFor(dto => dto.Image.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");

        RuleFor(dto => dto.Region).NotNull().NotEmpty().WithMessage("Region field is required!")
            .MinimumLength(3).WithMessage("Region must be more than 3 characters")
            .MaximumLength(50).WithMessage("Region must be less than 50 characters");

        RuleFor(dto => dto.District).NotNull().NotEmpty().WithMessage("District field is required!")
            .MinimumLength(3).WithMessage("District must be more than 3 characters")
            .MaximumLength(50).WithMessage("District must be less than 50 characters");

        RuleFor(dto => dto.Address).NotNull().NotEmpty().WithMessage("Address field is required!")
            .MinimumLength(3).WithMessage("Address must be more than 3 characters")
            .MaximumLength(50).WithMessage("Address must be less than 50 characters");

        RuleFor(dto => dto.Destination).NotNull().NotEmpty().WithMessage("Destination field is required!")
            .MinimumLength(3).WithMessage("Destination must be more than 3 characters")
            .MaximumLength(50).WithMessage("Destination must be less than 50 characters");

        RuleFor(dto => dto.ContactPhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");
    }
}
