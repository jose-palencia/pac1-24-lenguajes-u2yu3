using todo_list_backend.Dtos;
using todo_list_backend.Dtos.Tasks;
using todo_list_backend.Entities;

namespace todo_list_backend.Services.Interfaces
{
    public interface ITasksService
    {
        Task<ResponseDto<TaskDto>> CreateAsync(TaskCreateDto model);
        Task<ResponseDto<TaskDto>> EditAsync(TaskEditDto dto, Guid id);
        Task<ResponseDto<List<TaskDto>>> GetListAsync(string searchTerm = "");
        Task<ResponseDto<TaskDto>> GetOneByIdAsync(Guid id);
        Task<ResponseDto<TaskDto>> DeleteAsync(Guid id);
    }
}
