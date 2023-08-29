using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyDemo.Core.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MyDemo.Core.Services;

public class JwtService
{
    private readonly IConfiguration _configuration;
    public JwtService(
    IConfiguration configuration
   
    )
    {
        _configuration = configuration;
    }
    public JwtSecurityToken GetToken(User user)
    {
        var jwtOptions = new JwtSecurityToken(
                        issuer: _configuration["JwtSettings:Issuer"],
                        audience: _configuration["JwtSettings:Audience"],
                        claims: GetClaims(user),
                        expires: DateTime.Now.AddMinutes(Convert.ToDouble(
                                _configuration["JwtSettings:ExpirationTimeInMinutes"])),
                        signingCredentials: GetSigningCredentials());
        return jwtOptions;
    }
    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> GetClaims(User user)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };

        return claims;
    }

}
