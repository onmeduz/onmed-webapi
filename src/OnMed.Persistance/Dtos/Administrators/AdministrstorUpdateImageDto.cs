using Microsoft.AspNetCore.Http;

namespace OnMed.Persistance.Dtos.Administrators;

public class AdministrstorUpdateImageDto
{
    public IFormFile Image { get; set; } = default!;
}
