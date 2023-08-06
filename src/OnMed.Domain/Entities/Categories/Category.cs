namespace OnMed.Domain.Entities.Categories;

public class Category : Auditable
{
    public string ImagePath { get; set; } = string.Empty;

    public string Professionality { get; set; } = string.Empty;

    public string ProfessionalityHint { get; set; } = string.Empty;

    public string Professional { get; set; } = string.Empty;

    public string ProfessionalHint { get; set; } = string.Empty;
}
