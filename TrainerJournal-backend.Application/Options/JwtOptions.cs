using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TrainerJournal_backend.Application.Options;

public class JwtOptions
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpiryMinutes { get; set; }
    public string Secret { get; set; } = null!;
    
    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
    }
}