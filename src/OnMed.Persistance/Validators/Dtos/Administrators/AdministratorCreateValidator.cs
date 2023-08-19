using FluentValidation;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Administrators;

namespace OnMed.Persistance.Validators.Dtos.Administrators;

public class AdministratorCreateValidator : AbstractValidator<AdministratorCreateDto>
{
    public AdministratorCreateValidator()
    {
        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("FirstName field is required!")
            .MinimumLength(3).WithMessage("FirstName must be more than 3 characters")
            .MaximumLength(50).WithMessage("FirstName must be less than 50 characters");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("LastName field is required!")
            .MinimumLength(3).WithMessage("LastName must be more than 3 characters")
            .MaximumLength(50).WithMessage("LastName must be less than 50 characters");

        RuleFor(dto => dto.MiddleName).NotNull().NotEmpty().WithMessage("MiddleName field is required!")
            .MinimumLength(3).WithMessage("MiddleName must be more than 3 characters")
            .MaximumLength(50).WithMessage("MiddleName must be less than 50 characters");

        RuleFor(dto => dto.BirthDay).NotNull().NotEmpty().WithMessage("BirthDay field is required!");

        RuleFor(dto => dto.PhoneNumber).Must(phone => PhoneNumberValidator.IsValid(phone))
            .WithMessage("Phone number is invalid! ex: +998xxYYYAABB");

        RuleFor(dto => dto.Password).Must(password => PasswordValidator.IsStrongPassword(password).IsValid)
            .WithMessage("Password is invalid !");

        /*int maxImageSizeMB = 5;
        RuleFor(dto => dto.Image).NotEmpty().NotNull().WithMessage("Image field is required");
        RuleFor(dto => dto.Image.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1)
            .WithMessage($"Image size must be less than {maxImageSizeMB} MB");

        RuleFor(dto => dto.Image.FileName).Must(predicate =>
        {
            FileInfo fileInfo = new FileInfo(predicate);
            return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
        }).WithMessage("This file type is not image file");*/

        RuleFor(dto => dto.Region).NotNull().NotEmpty().WithMessage("Region field is required!")
            .MinimumLength(3).WithMessage("Region must be more than 3 characters")
            .MaximumLength(50).WithMessage("Region must be less than 50 characters");
    }
}
