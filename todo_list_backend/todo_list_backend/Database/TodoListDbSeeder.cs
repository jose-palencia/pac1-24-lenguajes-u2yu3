using Microsoft.AspNetCore.Identity;

namespace todo_list_backend.Database
{
    public static class TodoListDbSeeder
    {
        public static async Task LoadDataAsync(
            UserManager<IdentityUser> userManager,
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
                    var userAdmin = new IdentityUser 
                    {
                        Email = "jperez@me.com",
                        UserName = "jperez@me.com"
                    };

                    await userManager.CreateAsync(userAdmin, "MiMamaMeMima001*");
                    await userManager.AddToRoleAsync(userAdmin, "ADMIN");

                    var normalUser = new IdentityUser
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
