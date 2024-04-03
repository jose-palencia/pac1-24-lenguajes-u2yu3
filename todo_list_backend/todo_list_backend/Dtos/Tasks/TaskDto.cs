namespace todo_list_backend.Dtos.Tasks
{
    public class TaskDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool Done { get; set; }
    }
}
