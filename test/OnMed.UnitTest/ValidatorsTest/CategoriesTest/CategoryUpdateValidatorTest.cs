using Microsoft.AspNetCore.Http;
using OnMed.Persistance.Dtos.Categories;
using OnMed.Persistance.Validators.Dtos.Categories;
using System.Text;
using Xunit;

namespace OnMed.UnitTest.ValidatorsTest.CategoriesTest;

public class CategoryUpdateValidatorTest
{
    [Theory]
    [InlineData("djfkdkdlkdd", "sdjkfkdlodd", "djdkdkdd", "ajsjdjd")]
    [InlineData("dkdkckdkskd", "sdjkfkdlodd", "djdkdkdd", "ajsjdjd")]
    [InlineData("djfkdkdlkdd", "sdjkfkdlodd", "slkdlfkdklfk", "ajsjdjd")]
    [InlineData("mdkdkdkdkldkdkd", "sdjkfkdlodd", "skkdksmc", "ajsjdjd")]
    [InlineData("smskskskss", "s,xdskslsls", "sjdjjdjsajs", "ajsjdjd")]


    public void ValidCategoryUpdateDto_ReturnsNoValidationErrors(
        string professionality, string professionalityHint, string professional, string professionalHint)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", "file.jpg");

        var dto = new CategoryUpdateDto
        {
            Image = imageFile,
            Professionality = professionality,
            ProfessionalityHint = professionalityHint,
            Professional = professional,
            ProfessionalHint = professionalHint
        };

        var validationRules = new CategoryUpdateValidator();
        var result = validationRules.Validate(dto);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("", "sdjkfkdlodd", "djdkdkdd", "ajsjdjd")]
    [InlineData("dkdkckdkskd", "sdjkfkdlodd", "", "ajsjdjd")]
    [InlineData("", "sdjkfkdlodd", "slkdlfkdklfk", "ajsjdjd")]
    [InlineData("mdkdkdkdkldkdkd", "sdjkfkdlodd", "", "ajsjdjd")]
    [InlineData("", "sdjkfkdlodd", "", "ajsjdjd")]


    public void InvalidCategoryUpdateDto_ReturnsNoValidationErrors(
        string professionality, string professionalityHint, string professional, string professionalHint)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s");
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, byteImage.Length, "data", "file.jpg");

        var dto = new CategoryUpdateDto
        {
            Image = imageFile,
            Professionality = professionality,
            ProfessionalityHint = professionalityHint,
            Professional = professional,
            ProfessionalHint = professionalHint
        };

        var validationRules = new CategoryUpdateValidator();
        var result = validationRules.Validate(dto);

        Assert.False(result.IsValid);
    }
}
