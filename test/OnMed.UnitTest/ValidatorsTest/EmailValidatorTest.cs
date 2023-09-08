using OnMed.Persistance.Validators;
using Xunit;

namespace OnMed.UnitTest.ValidatorsTest;

public class EmailValidatorTest
{

    [Theory]
    [InlineData("ahadulla@gmail.com")]
    [InlineData("abdullaev@gmail.com")]
    [InlineData("rahmonovahadulla@umail.uz")]
    [InlineData("gvar1fjd@gmail.com")]
    [InlineData("akmalov@gmail.com")]
    [InlineData("ahadullajon@gmail.com")]
    [InlineData("ahadulla1503@gmail.com")]
    [InlineData("example@gmail.com")]
    [InlineData("ishlaydigan@gmail.com")]
    [InlineData("emailgmail@gmail.com")]

    public void ShoulReturnCorrect(string email)
    {
        var result = EmailValidator.IsValidEmail(email);
        Assert.True(result);
    }

    [Theory]
    [InlineData("ahadullagmail.com")]
    [InlineData("Abdullaev@gmailcom")]
    [InlineData("       @umail.uz")]
    [InlineData("AHSHDHSDJ@.com")]
    [InlineData("akmalov@gmail.")]
    [InlineData("gmailcom")]
    [InlineData("salom")]
    [InlineData("")]
    [InlineData("ishlaydigan%gmail.com")]
    [InlineData("gmail.com")]

    public void ShouldReturnWrong(string email)
    {
        var result = EmailValidator.IsValidEmail(email);
        Assert.False(result);
    }
}
