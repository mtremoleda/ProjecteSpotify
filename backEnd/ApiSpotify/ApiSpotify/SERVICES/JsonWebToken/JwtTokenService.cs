

using ApiSpotify.MODELS;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JswTokenService
{
    private readonly string _secret;

    public JswTokenService(IConfiguration config)
    {
        _secret = config["Jwt:JwtSecretKey"]
            ?? throw new Exception("Jwt SecretKey missing");
    }

    public string GenerateToken(
        string userId,
        string nom,
        string issuer,
        List<string> rols,
        string audience,
        TimeSpan lifetime)
    {
        SymmetricSecurityKey key =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

        SigningCredentials credentials =
            new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, nom), //utilitzem nom com a email.
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, nom),
            
        };

        foreach (var rol in rols)
        {
            claims.Add(new Claim(ClaimTypes.Role, rol));
        }

        DateTime now = DateTime.UtcNow;

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: now,
            expires: now.Add(lifetime),
            signingCredentials: credentials
        );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(token);
    }

    public List<Claim> ValidateAndGetClaimsFromToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler =
            new JwtSecurityTokenHandler();

        byte[] key = Encoding.UTF8.GetBytes(_secret);

        TokenValidationParameters validationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),

                ValidateIssuer = true,
                ValidIssuer = "demo",

                ValidateAudience = true,
                ValidAudience = "public",

                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30)
            };

        SecurityToken validatedToken;

        ClaimsPrincipal principal =
            tokenHandler.ValidateToken(
                token,
                validationParameters,
                out validatedToken
            );

        return principal.Claims.ToList();
    }
}

