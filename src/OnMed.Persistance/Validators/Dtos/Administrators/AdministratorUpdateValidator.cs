using FluentValidation;
using OnMed.Persistance.Dtos.Administrators;

namespace OnMed.Persistance.Validators.Dtos.Administrators;

public class AdministratorUpdateValidator : AbstractValidator<AdministratorUpdateDto>
{
    public AdministratorUpdateValidator()
    {
        
    }
}
