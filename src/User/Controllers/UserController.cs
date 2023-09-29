using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using User.Models;

namespace User.Controllers
{
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }

        [HttpPost("/user/register")]
        public async Task<IActionResult> Registration(UserCreateModel model) 
        {
            var user = await _userService.CreateAsync(model.Username, model.Password);

            if (user is null)
                return BadRequest();

            return Ok(new UserModel { Id = user.Id});
        }
    }
}
