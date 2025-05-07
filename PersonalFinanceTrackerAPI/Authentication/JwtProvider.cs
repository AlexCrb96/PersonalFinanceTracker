using Microsoft.IdentityModel.Tokens;
using PersonalFinanceTrackerDataAccess.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PersonalFinanceTrackerAPI.Authentication
{
    public class JwtProvider
    {
        private readonly IConfiguration _configuration;

        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]);
            return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
        }

        private string GenerateToken(IEnumerable<Claim> claims, DateTime expires)
        {
            var credentials = GetSigningCredentials();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.UtcNow,
                Expires = expires,
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateAuthToken(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            return GenerateToken(claims, DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryMinutes"])));
        }

        public string GenerateInvitationToken(FamilyInvitation invitation)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, invitation.SenderId),
                new Claim(ClaimTypes.Email, invitation.RecipientEmail),
                new Claim("FamilyId", invitation.FamilyId.ToString()),
                new Claim("InvitationStatus", invitation.Status.ToString())
            };

            return GenerateToken(claims, DateTime.UtcNow.AddDays(7));
        }
    }
}
