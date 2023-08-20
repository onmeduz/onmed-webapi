using Microsoft.AspNetCore.Http;

namespace OnMed.Persistance.Dtos.Users;

public class UploadImageDto
{
    public IFormFile Image { get; set; } = default!;
}
