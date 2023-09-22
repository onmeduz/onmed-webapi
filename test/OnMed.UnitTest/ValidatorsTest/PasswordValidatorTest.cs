using OnMed.Persistance.Validators;
using Xunit;

namespace OnMed.UnitTest.ValidatorsTest;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("StrongP@ss123")]
    [InlineData("123ABc!@")]
    [InlineData("aB2$Fgh8")]
    [InlineData("Abcd123!@#")]
    [InlineData("P@$$w0rd123")]
    [InlineData("AnotherValidP@ss1")]
    [InlineData("ValidP@ss1234")]
    [InlineData("Test123!@#")]
    [InlineData("Go@dPassw0rd")]
    [InlineData("Secure123!")]
    [InlineData("Pa$$w0rd1")]
    [InlineData("Passw0rd!")]
    [InlineData("Stron%g123")]
    [InlineData("C0mplexP@ssword")]
    [InlineData("P@ssw0rdSecure")]
    [InlineData("123!@#AaBbCc")]
    [InlineData("P@ssw0rdWithSymbols")]
    [InlineData("L0ngAndStr0ng!@#")]
    [InlineData("P@ssw0rdIsOK")]
    [InlineData("MyP@ssw0rd123")]


    public void CheckRight(string password)
    {
        var result = PasswordValidator.IsStrongPassword(password);
        Assert.True(result.IsValid);
        Assert.Equal(result.Message, result.Message);
    }

    [Theory]
    [InlineData("short")]
    [InlineData("Onase")]
    [InlineData("6789")]
    [InlineData("RCASE")]
    [InlineData("ls123!")]
    [InlineData("sHere!")]
    [InlineData("its123")]
    [InlineData("12345")]
    [InlineData("ord")]
    [InlineData("123")]
    [InlineData("y!@#")]
    [InlineData("45678")]
    [InlineData("lDigit")]
    [InlineData("")]
    [InlineData("fgft")]
    [InlineData("    ")]
    [InlineData("r123")]
    [InlineData("bers")]
    [InlineData("2345")]
    [InlineData("rt!@#")]

    public void CheckWrong(string password)
    {
        var result = PasswordValidator.IsStrongPassword(password);
        Assert.False(result.IsValid);
        Assert.Equal(result.Message.Trim(), result.Message);
    }
}
