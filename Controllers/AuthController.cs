using Microsoft.AspNetCore.Mvc;
using Service.AuthService;
using Service.Contract;
using SpanTechnologyTask.Utils;

namespace SpanTechnologyTask.Controllers
{
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [Route("/api/auth/login")]
        [HttpPost]
        public async Task<ActionResult<GenericResponse<string>>> Login([FromBody] LoginContract loginContract)
        {
            var result = await _authService.Login(loginContract);
            return Ok(new GenericResponse<string>(true, "JWT token has been created", result));
        }
    }
}
