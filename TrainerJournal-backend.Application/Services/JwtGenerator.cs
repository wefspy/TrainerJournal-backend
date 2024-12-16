using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TrainerJournal_backend.Application.Dtos;
using TrainerJournal_backend.Application.Options;

namespace TrainerJournal_backend.Application.Services;

public class JwtGenerator(IOptions<JwtOptions> options)
{
    private readonly JwtOptions _jwtOptions = options.Value;
    
    public string GenerateToken(User user)
    {
        var claims = CreateClaims(user);
        var signingCredentials = CreateSigningCredentials();
        var token = CreateJwtToken(claims, signingCredentials);

        var jwtTokenHandler = new JwtSecurityTokenHandler();
        return jwtTokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(
        List<Claim> claims, 
        SigningCredentials signingCredentials)
    {
        return new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes),
            signingCredentials: signingCredentials
        );
    }

    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
            _jwtOptions.GetSymmetricSecurityKey(), 
            SecurityAlgorithms.HmacSha256
        );
    }

    private static List<Claim> CreateClaims(User user)
    {
        if (user.UserName == null)
            throw new ArgumentNullException(nameof(user), "Username cannot be null");
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new(JwtRegisteredClaimNames.Name, user.MiddleName),
            new(JwtRegisteredClaimNames.Gender, user.Gender.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Prn, user.PhoneNumber),
        };
        
        claims.AddRange(
            user.Roles.Select(
                role => new Claim(ClaimTypes.Role, role)
            )
        );
        
        return claims;
    }
}