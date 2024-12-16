using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TrainerJournal_backend.Application.Options;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Application.Services.Jwt;

public class JwtGenerator(IOptions<JwtOptions> options)
{
    private readonly JwtOptions _jwtOptions = options.Value;
    
    public string GenerateToken(UserIdentity identity, IEnumerable<string> roles)
    {
        var claims = CreateClaims(identity, roles);
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

    private static List<Claim> CreateClaims(UserIdentity identity, IEnumerable<string> roles)
    {
        if (identity.UserName == null)
            throw new ArgumentNullException(nameof(identity), "Username cannot be null");
        
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, identity.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, identity.UserName),
        };
        
        claims.AddRange(
            roles.Select(
                role => new Claim(ClaimTypes.Role, role)
            )
        );
        
        return claims;
    }
}