using DeliveryService.API.Extensions;
using DeliveryService.API.Settings;
using DeliveryService.Common;
using DeliveryService.Database;
using DeliveryService.Database.Interfaces;
using DeliveryService.Database.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace DeliveryService.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.AddDbContext<DeliveryServiceDbContext>(options =>
                //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                options.UseSqlServer("Server=127.0.0.1,14330;Database=DeliveryServiceExercise;User Id=sa;Password=+YourStrong!Passw0rd+")
            );


            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingsSection["Secret"])),

                        ValidateIssuer = true,
                        ValidIssuer = appSettingsSection["Issuer"],

                        ValidateAudience = true,
                        ValidAudience = appSettingsSection["Audience"],

                        ValidateLifetime = true, //validate the expiration and not before values in the token

                        ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                    };
                });

            services.AddCors();

            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IUserManager, UserManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            DeliveryServiceDbContext context,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddLog4Net();

            var logger = loggerFactory.CreateLogger("WebAPI");
            app.ConfigureExceptionHandler(logger);

            //app.UseCors(builder => builder.WithOrigins("https://localhost:44352"));

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                //var context = serviceScope.ServiceProvider.GetRequiredService<DeliveryServiceDbContext>();
                context.Database.Migrate();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
