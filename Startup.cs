using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PayosferIdentity.Controllers;
using PayosferIdentity.Data;
using PayosferIdentity.Services;

namespace PayosferIdentity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtSettings = Configuration.GetSection("JwtSettings");
            var tokenSecretKey = jwtSettings.GetValue<string>("SecretKey");

            var key = Encoding.ASCII.GetBytes(tokenSecretKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddHealthChecks();
            services.AddAuthorization();
            services.AddMvc();

            services.AddScoped<PasswordResetService>();
            services.AddScoped<PasswordResetController>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<EmailService>();

            // TokenService'i yapılandırma dosyasından alarak kaydet
            

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyAllowSpecificOrigins",
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000")
                                             .AllowAnyHeader()
                                             .AllowAnyMethod();
                                  });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseOpenApi();

            app.UseCors("MyAllowSpecificOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
