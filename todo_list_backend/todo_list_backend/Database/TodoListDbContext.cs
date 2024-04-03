using Microsoft.EntityFrameworkCore;
using todo_list_backend.Entities;

namespace todo_list_backend.Database
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
