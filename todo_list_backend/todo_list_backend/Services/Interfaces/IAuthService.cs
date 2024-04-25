using todo_list_backend.Dtos;
using todo_list_backend.Dtos.Security;

namespace todo_list_backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync();
    }
}
