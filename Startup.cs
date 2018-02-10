using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TodoApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using TodoApi.Services;



namespace TodoApi
{
    
    public class Startup

    {
        public static IConfiguration Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<TodoContext>(opt =>
            opt.UseSqlite("Data Source=DB/Todo.db"));

            services.AddDbContext<UserContext>(opt =>
            opt.UseSqlite("Data Source=DB/Todo.db"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters =
                             new TokenValidationParameters
                             {
                                 ValidateIssuer = true,
                                 ValidateAudience = false,
                                 ValidateLifetime = false,
                                 ValidateIssuerSigningKey = true,

                                 ValidIssuer = "ToDo API",
                                 //ValidAudience = "Fiver.Security.Bearer",
                                 IssuerSigningKey = JwtSecurityKey.Create("Supersecret service kay mama")
                             };
                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                System.Console.WriteLine("OnAuthenticationFailed: " +
                                    context.Exception.Message);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context =>
                            {
                                System.Console.WriteLine("OnTokenValidated: " +
                                    context.SecurityToken);
                                return Task.CompletedTask;
                            }
                        };
                    });
            services.AddMvc();




        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMvc();

        }

    }
}