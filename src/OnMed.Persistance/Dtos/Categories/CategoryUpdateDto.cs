using Microsoft.AspNetCore.Http;

namespace OnMed.Persistance.Dtos.Categories;

public class CategoryUpdateDto
{
    public IFormFile? Image { get; set; }
    public string Professionality { get; set; } = string.Empty;
    public string ProfessionalityHint { get; set; } = string.Empty;
    public string Professional { get; set; } = string.Empty;
    public string ProfessionalHint { get; set; } = string.Empty;
}
