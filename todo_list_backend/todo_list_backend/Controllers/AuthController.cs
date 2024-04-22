using Microsoft.AspNetCore.Mvc;
using todo_list_backend.Dtos;
using todo_list_backend.Dtos.Security;
using todo_list_backend.Services.Interfaces;

namespace todo_list_backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController( IAuthService authService )
        {
            this._authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login (
            LoginDto dto
            ) 
        {
            var authResponse = await _authService.LoginAsync(dto);

            return StatusCode(authResponse.StatusCode, authResponse);
        } 
    }
}
