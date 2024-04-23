using AutoMapper;
using Microsoft.EntityFrameworkCore;
using todo_list_backend.Database;
using todo_list_backend.Dtos;
using todo_list_backend.Dtos.Tasks;
using todo_list_backend.Entities;
using todo_list_backend.Services.Interfaces;

namespace todo_list_backend.Services
{
    public class TasksService : ITasksService
    {
        private readonly TodoListDbContext _context;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;
        private readonly string _USER_ID;
        public TasksService(
            TodoListDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContextAccessor.HttpContext;
            var idClaim = _httpContext.User.Claims.Where(x => x.Type == "UserId")
                .FirstOrDefault();
            _USER_ID = idClaim?.Value;
        }

        public async Task<ResponseDto<List<TaskDto>>> GetListAsync(string searchTerm = "") 
        {
            var tasksEntity = await _context.Tasks
                .Where(t => t.Description.Contains(searchTerm) &&
                    t.UserId == _USER_ID).ToListAsync();

            var tasksDto = _mapper.Map<List<TaskDto>>(tasksEntity);

            return new ResponseDto<List<TaskDto>> 
            {
                Status = true,
                StatusCode = 200,
                Message = "Datos obtenidos correctamente",
                Data = tasksDto
            };
        }

        public async Task<ResponseDto<TaskDto>> GetOneByIdAsync(Guid id) 
        {
            var taskEntity = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == _USER_ID);

            if (taskEntity is null) 
            {
                return new ResponseDto<TaskDto>
                {
                    Status = true,
                    StatusCode = 404,
                    Message = $"Tarea {id} no encontrada."
                };
            }

            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return new ResponseDto<TaskDto>
            {
                Status = true,
                StatusCode = 200,
                Message= $"Tarea {taskDto.Id} encontrada.",
                Data = taskDto
            };
        }

        public async Task<ResponseDto<TaskDto>> CreateAsync(TaskCreateDto model)
        {
            var taskEntity = _mapper.Map<TaskEntity>(model);

            taskEntity.UserId = _USER_ID;

            _context.Tasks.Add(taskEntity);
            await _context.SaveChangesAsync();

            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return new ResponseDto<TaskDto>
            {
                Status = true,
                StatusCode = 201,
                Message = "Tarea creada correctamente",
                Data = taskDto
            };
        }

        public async Task<ResponseDto<TaskDto>> EditAsync(TaskEditDto dto, Guid id) 
        {
            var taskEntity = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == _USER_ID);

            if (taskEntity is null) 
            {
                return new ResponseDto<TaskDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = $"Tarea {id} no encontrada"
                };
            }

            _mapper.Map<TaskEditDto, TaskEntity>(dto, taskEntity);

            _context.Update(taskEntity);
            await _context.SaveChangesAsync();

            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return new ResponseDto<TaskDto> 
            {
                StatusCode = 200,
                Status = true,
                Message = $"La tarea {id} ha sido modificada.",
                Data = taskDto
            };
        }

        public async Task<ResponseDto<TaskDto>> DeleteAsync(Guid id) 
        {
            var taskEntity = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == _USER_ID);

            if (taskEntity is null) 
            {
                return new ResponseDto<TaskDto> 
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "Tarea no encontrada."
                };
            }

            _context.Remove(taskEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<TaskDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Tarea borrada correctamente."
            };
        }

    }
}
