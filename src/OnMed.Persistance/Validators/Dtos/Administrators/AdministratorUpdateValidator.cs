using FluentValidation;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Administrators;

namespace OnMed.Persistance.Validators.Dtos.Administrators;

public class AdministratorUpdateValidator : AbstractValidator<AdministratorUpdateDto>
{
    public AdministratorUpdateValidator()
    {
        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("FirstName field is required!")
            .MinimumLength(3).WithMessage("FirstName must be more than 3 characters")
            .MaximumLength(20).WithMessage("FirstName must be less than 20 characters");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("LastName field is required!")
            .MinimumLength(3).WithMessage("LastName must be more than 3 characters")
            .MaximumLength(20).WithMessage("LastName must be less than 20 characters");

        RuleFor(dto => dto.MiddleName).NotNull().NotEmpty().WithMessage("MiddleName field is required!")
            .MinimumLength(3).WithMessage("MiddleName must be more than 3 characters")
            .MaximumLength(20).WithMessage("MiddleName must be less than 20 characters");

        int maxImageSizeMB = 5;
        RuleFor(dto => dto.Image.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1)
            .WithMessage($"Image size must be less than {maxImageSizeMB} MB");

        RuleFor(dto => dto.Image.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");

        RuleFor(dto => dto.Region).NotNull().NotEmpty().WithMessage("Region field is required!")
            .MinimumLength(3).WithMessage("Region must be more than 3 characters");
    }
}
