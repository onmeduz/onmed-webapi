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
    [InlineData("Onlylowercase")]
    [InlineData("123456789")]
    [InlineData("ALLUPPERCASE")]
    [InlineData("nocapitals123!")]
    [InlineData("NoDigitsHere!")]
    [InlineData("AllDigits123")]
    [InlineData("NoSymbol12345")]
    [InlineData("Password")]
    [InlineData("Weak123")]
    [InlineData("SymbolsOnly!@#")]
    [InlineData("12345678")]
    [InlineData("NoSymbolOrDigit")]
    [InlineData("")]
    [InlineData("sfsdvdfbfgbfgfgft")]
    [InlineData("    ")]
    [InlineData("UPPERlower123")]
    [InlineData("PasswordWithoutNumbers")]
    [InlineData("12345")]
    [InlineData("Short!@#")]

    public void CheckWrong(string password)
    {
        var result = PasswordValidator.IsStrongPassword(password);
        Assert.False(result.IsValid);
        Assert.Equal(result.Message.Trim(), result.Message);
    }
}
