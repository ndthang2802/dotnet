using Application.services.token;
using Application.helpers;
using BC = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Collections.Generic;
using System;
using Application.services.user;
using Application.model;
namespace Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ITokenService _tokenService;
        private readonly DataContext _dataContext;
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config, ITokenService tokenService,DataContext dataContext)
        {
            this._tokenService = tokenService;
            this._dataContext = dataContext;
            this._config = config;
        }


        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid client request");
            }
            var user = _dataContext.Users.FirstOrDefault(u => 
                u.username == loginModel.username
            );
            if (user == null ){
                return Unauthorized();
            }
            if (BC.Verify(loginModel.password,user.password) == false )
            {
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
                new Claim("username", loginModel.username)
            };


            var accessToken = _tokenService.GenerateAccessToken(claims,_config["Jwt:key"].ToString(),_config["Jwt:iss"].ToString());
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(10);

            var success = _dataContext.SaveChanges();


            var userInfo = new UserModel(user.Id,user.username,user.phoneNumber,user.displayName);
            
            if (success > 0){
                return Ok(
                    new {
                        Token = accessToken,
                        RefreshToken = refreshToken,
                        info = userInfo
                    }
                );
            }
            else {
                return BadRequest("Something wrong");
            }
        }
        [HttpPost, Route("refresh")]

        public IActionResult refreshToken([FromBody] TokenModel tokenModel ) {
            try {
                TokenModel newToken = _tokenService.Refresh(tokenModel.accessToken,tokenModel.refreshToken);
                return Ok(newToken);
            }
            catch {
                return BadRequest("Something wrong");
            }
        }
    }
}
