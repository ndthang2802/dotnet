using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;


namespace Application.helpers
{
    public class JwtMiddleware {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;
        public JwtMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            

            if (token != null)
                attachUserToContext(context, token);


            await _next(context);
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
                Console.WriteLine("fail");
                throw new SecurityTokenException("Invalid token");
            }
        }

        private void attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_config["Jwt:key"]);
        
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _config["Jwt:iss"],
                    ValidAudience = _config["Jwt:iss"],
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                
                var jwtToken = (JwtSecurityToken)validatedToken;
        
                var username = jwtToken.Claims.First(x => x.Type == "username").Value;

                // attach user to context on successful jwt validation
                context.Items["User"] = username;
            }
            catch (SecurityTokenExpiredException) // token has expired
            {
                    
                context.Items["Token_Expired"] = true;
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}