using FluentValidation;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Categories;

namespace OnMed.Persistance.Validators.Dtos.Categories
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(dto => dto.Professionality).NotNull().NotEmpty().WithMessage("Professionality field is required!")
            .MinimumLength(3).WithMessage("Professionality must be more than 3 characters")
            .MaximumLength(50).WithMessage("Professionality must be less than 50 characters");
            RuleFor(dto => dto.Professional).NotNull().NotEmpty().WithMessage("Professional field is required!")
                .MinimumLength(3).WithMessage("Professional must be more than 3 characters")
                .MaximumLength(50).WithMessage("Professional must be less than 50 characters");

            int maxImageSizeMB = 5;
            RuleFor(dto => dto.Image).NotEmpty().NotNull().WithMessage("Image field is required");
            RuleFor(dto => dto.Image.Length).LessThan(maxImageSizeMB * 1024 * 1024 + 1)
                .WithMessage($"Image size must be less than {maxImageSizeMB} MB");
            RuleFor(dto => dto.Image.FileName).Must(predicate =>
            {
                FileInfo fileInfo = new FileInfo(predicate);
                return MediaHelper.GetImageExtensions().Contains(fileInfo.Extension);
            }).WithMessage("This file type is not image file");
        }
    }
}
