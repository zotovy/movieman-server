using System.Text;
using Database;
using Database.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Services.ExternalMovieApi;
using Services.User;

namespace API {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<DatabaseContext>(options =>
                options.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=7852")
            );
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IExternalMovieApiService, ExternalMovieApiService>();
            services.AddControllers();
            
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            
            services.AddApiVersioning(x =>  
            {  
                x.DefaultApiVersion = new ApiVersion(1, 0);  
                x.AssumeDefaultVersionWhenUnspecified = true;  
                x.ReportApiVersions = true;  
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                // app.UseStatusCodePages();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
        }
    }
}