using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
namespace Application.services.token
{
    public class TokenService : ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 20;

        public TokenService()
        {

        }

        public string GenerateAccessToken(IEnumerable<Claim> claims,string key,string issuer)
        {   
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer,issuer,claims,expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES),signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}