using App.InputModels;
using App.Repository;
using App.ResponseModels;
using App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private IUserRepository userRepository;
        private ILoginService loginService;

        public LoginController(ILogger<LoginController> logger, IUserRepository userRepository, ILoginService loginService)
        {
             this.userRepository = userRepository;
             this.loginService= loginService;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<UserResponse> Login([FromBody] UserLogin model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var res = loginService.Login(model);
                if (res != null)
                {
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }
        [Authorize]
        [HttpGet]
        public ActionResult<UserResponse> GetUser()
        {
            try
            {
                var res = loginService.GetUser(HttpContext.User.Claims.FirstOrDefault(x=>x.Type == "name").Value);
                if (res != null)
                {
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
        }
    }
}
