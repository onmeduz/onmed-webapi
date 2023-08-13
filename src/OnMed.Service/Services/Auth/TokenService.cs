using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnMed.Domain.Entities.Administrators;
using OnMed.Domain.Entities.Doctors;
using OnMed.Domain.Entities.Heads;
using OnMed.Domain.Entities.Users;
using OnMed.Persistance.Common.Helpers;
using OnMed.Service.Interfaces.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnMed.Service.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration configuration)
    {
        _config = configuration.GetSection("Jwt");
    }

    public string GenerateToken(User user)
    {
        var identityClaims = new Claim[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("Lastname", user.LastName),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.Role, "User")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        int expiresHours = int.Parse(_config["Lifetime"]!);
        var token = new JwtSecurityToken(
            issuer: _config["Issuer"],
            audience: _config["Audience"],
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateToken(Administrator admin)
    {
        throw new NotImplementedException();
    }

    public string GenerateToken(Doctor doctor)
    {
        var identityClaims = new Claim[]
        {
            new Claim("Id", doctor.Id.ToString()),
            new Claim("FirstName", doctor.FirstName),
            new Claim("Lastname", doctor.LastName),
            new Claim(ClaimTypes.MobilePhone, doctor.PhoneNumber),
            new Claim(ClaimTypes.Role, "Doctor")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKey"]!));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        int expiresHours = int.Parse(_config["Lifetime"]!);
        var token = new JwtSecurityToken(
            issuer: _config["Issuer"],
            audience: _config["Audience"],
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateToken(Head head)
    {
        throw new NotImplementedException();
    }
}
