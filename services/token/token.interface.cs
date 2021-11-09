using System.Collections.Generic;
using System.Security.Claims;
using  Application.model;
namespace Application.services.token
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> Claim,string key,string issuer);
        string GenerateRefreshToken();
        TokenModel Refresh(string token,string refreshToken);
    }

}