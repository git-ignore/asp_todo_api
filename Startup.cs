using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TodoApi.Models;
using Microsoft.IdentityModel.Tokens;
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

            services.AddMvc();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters =
                            new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateIssuerSigningKey = true,

                                ValidIssuer = "ToDo API",
                                IssuerSigningKey = JwtSecurityKey.Create("Supersecret service kay mama")
                            };
                    });
        }


        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}