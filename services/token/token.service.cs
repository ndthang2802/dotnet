using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Application.helpers;
using System.Security.Cryptography;
using System.Linq;
using Application.entities;
using Application.model;
namespace Application.services.token
{
    public class TokenService : ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 5;
        private readonly IConfiguration _config;

         private DataContext _dataContext;
        public TokenService(IConfiguration config,DataContext dataContext)
        {
            _config=config;
            _dataContext= dataContext;
        }

        private ClaimsPrincipal GetClaimsPrincipalFromExpriredToken(string token){
            try {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["Jwt:key"]);
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _config["Jwt:iss"],
                        ValidAudience = _config["Jwt:iss"],
                        ValidateLifetime = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                return principal;
            }
            catch {
                throw new SecurityTokenException("Invalid token");
            }
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

        public TokenModel Refresh(string token, string refreshToken){
            try {
                var jwtToken = GetClaimsPrincipalFromExpriredToken(token); // get token from expired token
                var username = jwtToken.Claims.First(x => x.Type == "username").Value;
                User user =  _dataContext.Users.FirstOrDefault(u => u.username == username);
                if (refreshToken != user.RefreshToken){
                    throw new SecurityTokenException("Invalid refresh token");
                }
                DateTime expiredDay = user.RefreshTokenExpiryTime;
                DateTime now = DateTime.Now;
                if (now > expiredDay) {
                    throw new SecurityTokenException("Refresh token expired");
                }
                var claims = new List<Claim>
                    {
                        new Claim("username", user.username)
                    };

                var newaccessToken = GenerateAccessToken(claims,_config["Jwt:key"].ToString(),_config["Jwt:iss"].ToString());
                // var newrefreshToken = GenerateRefreshToken();

                // user.RefreshToken = newrefreshToken;

                // var success = _dataContext.SaveChanges();

                // if (success > 0){
                return new TokenModel(newaccessToken,user.RefreshToken);
                // }

                // else {
                //     throw new Exception("fix later");
                // }
            }
            catch (SecurityTokenException) {
                throw new SecurityTokenException("Invalid token");
            }
        }
    }
}