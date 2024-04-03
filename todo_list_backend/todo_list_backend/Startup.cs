using Microsoft.EntityFrameworkCore;
using todo_list_backend.Database;
using todo_list_backend.Services;
using todo_list_backend.Services.Interfaces;

namespace todo_list_backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddControllers();

            // Add DbContext
            services.AddDbContext<TodoListDbContext>( options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add Custom Services
            services.AddTransient<ITasksService, TasksService>();

            // Add Automapper Service
            services.AddAutoMapper(typeof(Startup));

            services.AddCors(options => 
            {
                options.AddDefaultPolicy(builder => 
                {
                    builder.WithOrigins(Configuration["FrontendURL"])
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });
        }
    }
}
