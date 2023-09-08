using OnMed.Persistance.Dtos.Users;
using OnMed.Persistance.Validators.Dtos.Users;
using Xunit;

namespace OnMed.UnitTest.ValidatorsTest.UsersTest;

public class UserUpdateValidatorTest
{
    [Theory]
    [InlineData("Ahadulla", "Rahmonov","Sherqobol o'g'li","15/03/2001", true, "Surxondaryo")]
    [InlineData("Aziz", "Rahimov", "Karimovich", "28/03/2000", true, "Denov")]
    [InlineData("Bobur", "Qodirov", "alievich", "15/03/2001", true,"Namangan")]
    [InlineData("Muhammadali", "Karimov", "Nishonovich", "/15/04/4545", true, "Andijon")]
    [InlineData("Akmal", "Sobirov", "Abdullaevich", "14/04/4004", true, "Xorazm")]
    [InlineData("Qahramon", "Anvarov", "Aziz o'g'li", "/03/03/2001", true, "Qo'qon")]
    [InlineData("Ibodulla", "Adhamov", "Jo'raevich", "/14/12/2020", true, "Termiz")]
    [InlineData("Nozim", "Akbarov", "djkfffndnjf", "28/03/1996", true, "Samarqand")]
    [InlineData("Ali", "Abdullaev", "Ajdfhfhf", "15/11/1984", true, "Gijduvon")]
    [InlineData("Jahongir", "Qubonaliyev", "Elshod o'g'li", "14/01/1985", true, "Columbia")]

    public void ValidUserUpdateDto_ReturnsNoValidationErrors(
        string firstName, string lastName, string middleName, string birthDay, bool ismale, string region)
    {
        var dto = new UserUpdateDto
        {
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            BirthDay = birthDay,
            IsMale = ismale,
            Region = region
        };

        var validationRules = new UserUpdateValidator();
        var result = validationRules.Validate(dto);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("Ahadulla", "Rahmonov", "Sherqobol o'g'li", "", true, "Surxondaryo")]
    [InlineData("Aziz", "Rahimov", "Karimovich", "28/03/2000", true, "")]
    [InlineData("Bobur", "Qodirov", "", "45/16/2001", true, "Namangan")]
    [InlineData("Muhammadali", "", "Nishonovich", "/15/03/2001", true, "Andijon")]
    [InlineData("", "Sobirov", "Abdullaevich", "//////", true, "Xorazm")]
    [InlineData("Qahramon", "a", "Aziz o'g'li", "/14/15/5002", true, "Qo'qon")]
    [InlineData("r", "Adhamov", "Jo'raevich", "155656", true, "Termiz")]
    [InlineData("Nozim", "Akbarov", "djkfffndnjf", "hgfkjkh", true, "")]
    [InlineData("Ali", "Abdullaev", "", "454656546", true, "Gijduvon")]
    [InlineData("Jahongir", "Qubonaliyev", "Elshod o'g'li", "", true, "Columbia")]

    public void InvalidUserUpdateDto_ReturnsValidationErrors(
        string firstName, string lastName, string middleName, string birthDay, bool ismale, string region)
    {
        var dto = new UserUpdateDto
        {
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            BirthDay = birthDay,
            IsMale = ismale,
            Region = region
        };

        var validationRules = new UserUpdateValidator();
        var result = validationRules.Validate(dto);

        Assert.False(result.IsValid);
    }
}
