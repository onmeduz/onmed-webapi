﻿using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Validators.Dtos.Auth;
using Xunit;

namespace OnMed.UnitTest.ValidatorsTest.AuthTest;

public class LoginValidatorTest
{
    [Theory]
    [InlineData("+998951092161", "asAS@#%123")]
    [InlineData("+998971234567", "Abcd123!@#")]
    [InlineData("+998998877665", "P@$$w0rd123")]
    [InlineData("+998935555555", "Qwerty!123")]
    [InlineData("+998940404040", "Hello123!")]
    [InlineData("+998950505050", "MyP@ssw0rd")]
    [InlineData("+998960606060", "OnMed!23!")]
    [InlineData("+998970707070", "Testing@123")]
    [InlineData("+998980808080", "Welcom@123")]
    [InlineData("+998990909090", "AAaa@@11")]

    public void ValidLoginDto_ReturnsNoValidationErrors(string phone, string password)
    {
        var dto = new LoginDto()
        {
            PhoneNumber = phone,
            Password = password
        };

        var userLoginDto = new LoginValidator();
        var result = userLoginDto.Validate(dto);

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("-998951092161", "inval")]
    [InlineData("123456789", "asAS@#%123")]
    [InlineData("+998951092161", "")]
    [InlineData("invalidphone", "asAS@#%123")]
    [InlineData("+998951092161", "    ")]
    [InlineData("+99851092161", "1289")]
    [InlineData("1092161", "AJSJD")]
    [InlineData("+", "noters")]
    [InlineData("+99895jbdsdb1092161", "")]
    [InlineData("+99895145092161", "noDig")]

    public void InvalidLodinDto_ReturnsValidationErrors(string phone, string password)
    {
        var dto = new LoginDto()
        {
            PhoneNumber = phone,
            Password = password
        };

        var validator = new LoginValidator();
        var result = validator.Validate(dto);

        Assert.False(result.IsValid);
    }
}
