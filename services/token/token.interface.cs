using System;
using Application.model.Authentication;
using Application.model;
using System.Collections.Generic;
using System.Security.Claims;
namespace Application.services.token
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> Claim,string key,string issuer);
        string GenerateRefreshToken();
        
    }

}