using Application.services.user;
using Microsoft.AspNetCore.Mvc;
using Application.model;
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
        [HttpGet("userInfo")]
        [AuthorizeAttribute]
        public IActionResult GetUserInfo()
        {
            var userName = HttpContext.Items["User"];
            UserModel user = _userService.GetUserByUsername(userName.ToString());
            return Ok(
                user
            );
        }
    }
}
