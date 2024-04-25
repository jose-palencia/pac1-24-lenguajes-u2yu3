using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using todo_list_backend.Dtos;
using todo_list_backend.Dtos.Security;
using todo_list_backend.Entities;
using todo_list_backend.Services.Interfaces;

namespace todo_list_backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IConfiguration _configuration;
        private readonly HttpContext _httpContext;
        private readonly string _USER_ID;
        public AuthService(
            SignInManager<UserEntity> signInManager,
            UserManager<UserEntity> userManager,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _httpContext = httpContextAccessor.HttpContext;
            var idClaim = _httpContext.User.Claims.Where(x => x.Type == "UserId")
                .FirstOrDefault();
            _USER_ID = idClaim?.Value;
        }

        public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto) 
        {
            var result = await _signInManager.PasswordSignInAsync(
                dto.Email, 
                dto.Password, 
                isPersistent: false, 
                lockoutOnFailure: false    
            );

            if (result.Succeeded) 
            {
                var userEntity = await _userManager
                    .FindByEmailAsync( dto.Email );

                var authClaims = new List<Claim> 
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userEntity.Id),
                };

                var userRoles = await _userManager.GetRolesAsync(userEntity);

                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                var jwtToken = GetToken(authClaims);

                return new ResponseDto<LoginResponseDto> 
                {
                    StatusCode = 200,
                    Status = true,
                    Message = "Inicio de sesión realizado satisfactoriamente",
                    Data = new LoginResponseDto 
                    {
                        Email = userEntity.Email,
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                        TokenExpiration = jwtToken.ValidTo,
                    }
                };

            }

            return new ResponseDto<LoginResponseDto> 
            {
                StatusCode = 400,
                Status = false,
                Message = "Fallo el inicio de sesión"
            };
        }

        public async Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync() 
        {
            var userEntity = await _userManager.FindByIdAsync(_USER_ID);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userEntity.Id),
                };

            var userRoles = await _userManager.GetRolesAsync(userEntity);

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = GetToken(authClaims);

            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Token renovado satisfactoriamente",
                Data = new LoginResponseDto
                {
                    Email = userEntity.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    TokenExpiration = jwtToken.ValidTo,
                }
            };
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigninKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigninKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
