using AutoMapper;
using todo_list_backend.Dtos.Tasks;
using todo_list_backend.Entities;

namespace todo_list_backend.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForTasks();
        }

        private void MapsForTasks() 
        {
            CreateMap<TaskEntity, TaskDto>();
            CreateMap<TaskCreateDto, TaskEntity>();
            CreateMap<TaskEditDto, TaskEntity>();
        }
    }
}
