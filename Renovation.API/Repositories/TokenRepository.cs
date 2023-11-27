using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Renovation.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;
        public TokenRepository(IConfiguration configuration) {
            this.configuration = configuration;    
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //Create claims from roles
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };


            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create a symmetric security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            // Create signing credentials using the key and HMAC-SHA256 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create JWT token with issuer, audience, claims, expiration, and signing credentials
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            // Serialize the JWT token to a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
