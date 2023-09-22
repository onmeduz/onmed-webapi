using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Validators.Dtos.Auth;
using Xunit;

namespace OnMed.UnitTest.ValidatorsTest.AuthTest;

public class RegidterValidatorTest
{
    [Theory]
    [InlineData("Ahadulla", "Rahmonov", "+998335103545", "AAaa@@11")]
    [InlineData("Aziz", "Rahimov", "+998335103546", "AAaa@@11")]
    [InlineData("Bobur", "Qodirov", "+998335107545", "AAaa@@11")]
    [InlineData("Muhammadali", "Karimov", "+998338103545", "AAaa@@11")]
    [InlineData("Akmal", "Sobirov", "+998335103544", "AAaa@@11")]
    [InlineData("Qahramon", "Anvarov", "+998335108545", "AAaa@@11")]
    [InlineData("Ibodulla", "Adhamov", "+998335106545", "AAaa@@11")]
    [InlineData("Nozim", "Akbarov", "+998335103543", "AAaa@@11")]
    [InlineData("Ali", "Abdullaev", "+998335103547", "AAaa@@11")]
    [InlineData("Jahongir", "Qubonaliyev", "+998905103545", "AAaa@@11")]

    public void ValidRegisterDto_ReturnsNoValidationErrors(
        string firstName, string lastName, string phoneNumber, string password)
    {
        var dto = new RegisterDto
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Password = password
        };

        var validationRules = new RegisterValidator();
        var result = validationRules.Validate(dto);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("Ahadulla", "Rahmonov", "+998335103545545", "AA11")]
    [InlineData("Aziz", "Rahimov", "+99833510347474546", "@@#%$^")]
    [InlineData("", "Qodirov", "+998335107545", "1111")]
    [InlineData("Muhammadali", "", "+998338103545", "AAAAA")]
    [InlineData("Akmal", "Sobirov", "+998335103544", "      ")]
    [InlineData("Qahramon", "Anvarov", "-998335108545", "AA")]
    [InlineData("I", "Adhamov", "+9978335106545", "AAAA11")]
    [InlineData("Nozim", "a", "+998335103543", "hha@11")]
    [InlineData("Ali", "Abdullaev", "+99845335103547", "AAa@ii")]
    [InlineData("Jahongir", "Qubonaliyev", "+998905103545", "AA1")]

    public void InvalidRegisterDto_ReturnsValidationErrors(
        string firstName, string lastName, string phoneNumber, string password)
    {
        var dto = new RegisterDto
        {
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Password = password
        };

        var validationRules = new RegisterValidator();
        var result = validationRules.Validate(dto);

        Assert.False(result.IsValid);
    }
}
