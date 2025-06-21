using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.RequestHandler.Requests;
using TodoList.Services;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTService _jwtService;
        public AccountController(JWTService jWTService)
        {
            _jwtService = jWTService;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _jwtService.Authenticate(request);
            if (result is null) return Unauthorized();

            return Ok(result);
        }
    }
}
