using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using todo_list_backend.Database;
using todo_list_backend.Entities;
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
            services.AddTransient<IAuthService, AuthService>();

            // Add Automapper Service
            services.AddAutoMapper(typeof(Startup));

            // Add Identity
            services.AddIdentity<UserEntity, IdentityRole>(options => 
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<TodoListDbContext>()
            .AddDefaultTokenProviders();

            // Add Authentication
            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });
        }
    }
}
