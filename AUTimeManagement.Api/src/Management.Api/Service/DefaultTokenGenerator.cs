using AUTimeManagement.Api.Management.Api.Configuration;
using AUTimeManagement.Api.Management.Api.Security.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AUTimeManagement.Api.Management.Api.Service;

public sealed class DefaultTokenGenerator : ITokenGenerator
{
    private readonly IOptions<TokenGenerationOption> options;
    private readonly UserManager<ApplicationUser> userManager;

    public DefaultTokenGenerator(IOptions<TokenGenerationOption> options, UserManager<ApplicationUser> userManager)
    {
        if (options is null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        this.options = options;
        this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    }

    public async Task<string> GetToken(ApplicationUser user)
    {
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var isAdminTask = userManager.IsInRoleAsync(user, "Admin");

        var cfg = options.Value;

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg.SigningSecret));

        List<Claim> claims = new List<Claim>()
        {
            new Claim("Id", user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.UserName),
        };
        bool isAdmin = await isAdminTask;
        if (isAdmin)
        {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(cfg.ExpireDuration),
            Issuer = cfg.ValidIssuer,
            Audience = cfg.ValidAudience,
            SigningCredentials = new SigningCredentials(
                signingKey,
                SecurityAlgorithms.HmacSha512Signature
            )
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;
    }
}
