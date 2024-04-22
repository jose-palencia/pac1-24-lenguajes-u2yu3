using Microsoft.AspNetCore.Identity;
using todo_list_backend.Entities;

namespace todo_list_backend.Database
{
    public static class TodoListDbSeeder
    {
        public static async Task LoadDataAsync(
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
            ) 
        {
            try
            {
                if (!roleManager.Roles.Any()) 
                {
                    await roleManager.CreateAsync(new IdentityRole("USER"));
                    await roleManager.CreateAsync(new IdentityRole("ADMIN"));
                }

                if (!userManager.Users.Any()) 
                {
                    var userAdmin = new UserEntity 
                    {
                        Email = "jperez@me.com",
                        UserName = "jperez@me.com"
                    };

                    await userManager.CreateAsync(userAdmin, "MiMamaMeMima001*");
                    await userManager.AddToRoleAsync(userAdmin, "ADMIN");

                    var normalUser = new UserEntity
                    {
                        Email = "mperez@me.com",
                        UserName = "mperez@me.com"
                    };

                    await userManager.CreateAsync(normalUser, "MiMamaMeMima001*");
                    await userManager.AddToRoleAsync(normalUser, "USER");
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TodoListDbContext>();
                logger.LogError(e.Message);
            }
        }
    }
}
