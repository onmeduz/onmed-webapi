using FluentValidation;
using OnMed.Persistance.Dtos.Users;

namespace OnMed.Persistance.Validators.Dtos.Users;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(dto => dto.FirstName).NotNull().NotEmpty().WithMessage("Firstname is required!")
            .MinimumLength(2).WithMessage("FirstName must be more than 2 characters")
            .MaximumLength(20).WithMessage("Firstname must be less than 20 characters");

        RuleFor(dto => dto.LastName).NotNull().NotEmpty().WithMessage("Lastname is required!")
            .MinimumLength(2).WithMessage("LastName must be more than 2 characters")
            .MaximumLength(20).WithMessage("Lastname must be less than 20 characters");

        RuleFor(dto => dto.MiddleName).NotNull().NotEmpty().WithMessage("MiddleName field is required!")
            .MinimumLength(2).WithMessage("MiddleName must be more than 2 characters")
            .MaximumLength(20).WithMessage("MiddleName must be less than 20 characters");

        RuleFor(dto => dto.BirthDay).NotNull().NotEmpty().WithMessage("Birth day field is required!");

        RuleFor(dto => dto.IsMale).NotNull().NotEmpty().WithMessage("IsMale field is required!");

        RuleFor(dto => dto.Region).NotNull().NotEmpty().WithMessage("Region field is required!");
    }
}
