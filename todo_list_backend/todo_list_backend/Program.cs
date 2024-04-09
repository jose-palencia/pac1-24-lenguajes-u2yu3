using Microsoft.AspNetCore.Identity;
using todo_list_backend;
using todo_list_backend.Database;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

using (var scope = app.Services.CreateScope()) 
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

	try
	{
		var userManager = service.GetRequiredService<UserManager<IdentityUser>>();
		var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

		await TodoListDbSeeder.LoadDataAsync(userManager, roleManager, loggerFactory);
	}
	catch (Exception e)
	{
		var logger = loggerFactory.CreateLogger<Program>();
		logger.LogError(e, "Errro al inicializar datos.");
	}

}

    app.Run();
