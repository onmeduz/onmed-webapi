using Microsoft.AspNetCore.Http;
using OnMed.Persistance.Dtos.Users;
using OnMed.Persistance.Validators.Dtos.Users;
using System.Text;
using Xunit;

namespace OnMed.UnitTest.ValidatorsTest.UsersTest;

public class UploadImageValidatorTest
{
    [Theory]
    [InlineData(2.5,"file.jpg")]
    [InlineData(1.5,"file.png")]
    [InlineData(4.5,"file.svg")]
    [InlineData(3.5,"file.jpeg")]
    [InlineData(5,"file.bmp")]


    public void ValidUploadImageDto_ReturnsValidationErrors(float MaxImageSizeMB, string fileName)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("we sell an fnsdlfkdjldjkldkjbnflkjgjgjfgjfjfjkfjkfjfkljgrklgrtiojrlgjb vneklfjsflkm klkrofjeewrjo fo gj oerjoio rg rghriugjr grh tgelectronic products to our clients");
        long imageSizeInBytes = (long)(MaxImageSizeMB * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInBytes, "data", fileName);

        var dto = new UploadImageDto
        {
            Image = imageFile
        };

        var validationRules = new UploadImageValidator();
        var result = validationRules.Validate(dto);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(2.5, "file.jpsg")]
    [InlineData(5.5, "file.png")]
    [InlineData(4.5, "file.svgd")]
    [InlineData(15, "file.jpeg")]
    [InlineData(51, "file.bmp")]
    [InlineData(4, "j2pg")]
    [InlineData(2, "jpeg")]
    [InlineData(25, "file.png")]
    [InlineData(100, "file.bmp")]
    [InlineData(4.9, "svg")]

    public void InvalidUploadImageDto_ReturnsValidationErrors(float MaxImageSizeMB, string fileName)
    {
        byte[] byteImage = Encoding.UTF8.GetBytes("we sell an electronic products to our clients");
        long imageSizeInBytes = (long)(MaxImageSizeMB * 1024 * 1024);
        IFormFile imageFile = new FormFile(new MemoryStream(byteImage), 0, imageSizeInBytes, "data", fileName);

        var dto = new UploadImageDto
        {
            Image = imageFile
        };

        var validationRules = new UploadImageValidator();
        var result = validationRules.Validate(dto);

        Assert.False(result.IsValid);
    }
}
