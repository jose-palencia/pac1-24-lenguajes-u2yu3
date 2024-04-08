using Microsoft.AspNetCore.Identity;

namespace todo_list_backend.Database
{
    public class TodoListDbSeeder
    {
        public static async Task LoadDataAsync(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
            ) 
        {
            try
            {
    
            }
            catch (Exception e)
            {

                var logger = loggerFactory.CreateLogger<TodoListDbContext>();
                logger.LogError(e.Message);
            }
        }
    }
}
