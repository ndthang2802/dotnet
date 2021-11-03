using Application.services.user;
using Application.entities;
using Microsoft.AspNetCore.Mvc;
using Application.model.Authentication;
using Application.model.AppData;

using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;
namespace Application.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserService _userService;
        

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterModel user)
        {
            try
            {
                var res = _userService.Create(user);
                return StatusCode(201);
            }
            catch (System.Exception ex)
            {   
                if(ex.Message == "Username Taken"){
                    return BadRequest("Username taken");
                }
                else if (ex.Message=="Phone number taken"){
                    return BadRequest("Phone is already used");
                }
                else {
                    return BadRequest("Fail"+ex.Message);
                }
            }
            
        }
        [HttpGet("user")]
        
        public IActionResult GetUser(string username)
        {
            //List<User> user_l =  _userService.GetUserBy();
            UserModel user = _userService.GetUserByUsername(username);
            return Ok(
                user
            );
        }
    }
}
