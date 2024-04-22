using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todo_list_backend.Dtos;
using todo_list_backend.Dtos.Tasks;
using todo_list_backend.Services.Interfaces;

namespace todo_list_backend.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TasksController(
            ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<TaskDto>>>> GetAll(string searchTerm = "")
        {
            var tasksResponse = await _tasksService.GetListAsync(searchTerm);

            return StatusCode(tasksResponse.StatusCode, tasksResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<TaskDto>>> GetOneById(Guid id)
        {
            var taskResponse = await _tasksService.GetOneByIdAsync(id);

            return StatusCode(taskResponse.StatusCode, taskResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<TaskDto>>> Create([FromBody] TaskCreateDto model)
        {
            var tasksResponse = await _tasksService.CreateAsync(model);

            return StatusCode(tasksResponse.StatusCode, tasksResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<TaskDto>>> Edit(
            [FromBody] TaskEditDto dto, 
            Guid id
            ) 
        {
            var taskResponse = await _tasksService.EditAsync(dto, id);
            return StatusCode(taskResponse.StatusCode, taskResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<TaskDto>>> Delete(Guid id) 
        {
            var taskResponse = await _tasksService.DeleteAsync(id);
            return StatusCode(taskResponse.StatusCode, taskResponse);
        } 
    }
}
