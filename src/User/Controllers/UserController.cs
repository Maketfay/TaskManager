using Infrastructure.Entity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Models;
using ViewModel;

namespace User.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/user/register")]
        public async Task<IActionResult> Registration(UserCreateModel model)
        {
            var token = await _userService.CreateAsync(model.Username, model.Password);

            if (token is null)
                return BadRequest();

            return Ok(token);
        }

        [HttpPost("/user/login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var token = await _userService.AuthenticateAsync(model.Username, model.Password);

            if (token is null)
                return BadRequest();

            return Ok(token);
        }

        [HttpPost("/user/refresh")]
        public async Task<IActionResult> Refresh(TokenModel model) 
        {
            var token = await _userService.RefreshTokenAsync(model);

            if(token is null)
                return BadRequest();

            return Ok(token);
        }

        [Authorize]
        [HttpGet("/user/test")]
        public IActionResult Test(string message) 
        {
            return Ok(message);
        }
    }
}
