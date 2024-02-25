// Controllers/LoginController.cs

using Microsoft.AspNetCore.Mvc;
using PayosferIdentity.Models;
using PayosferIdentity.Services;
using System.Threading.Tasks;

namespace PayosferIdentity.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await _userService.LoginAsync(loginModel.Email, loginModel.Password);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("Login failed");
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
