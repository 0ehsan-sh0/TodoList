using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Database.Interfaces;
using TodoList.Database.Models;
using TodoList.RequestHandler.Requests;
using TodoList.Services;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(JWTService jWTService, IUserRepository userRepository) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await userRepository.LoginAsync(request.Username, request.Password);
            if (user is null) return Unauthorized();

            var result = jWTService.Authenticate(request);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await userRepository.RegisterAsync(new User { username = request.Username, password = request.Password });
            if (result is null) return Unauthorized();

            var loginResponce = jWTService.Authenticate(new LoginRequest { Username = result.username, Password = request.Password });
            return Ok(loginResponce);
        }
    }
}
