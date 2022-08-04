using System;
using Football.Core.Persistence.Interfaces.DataProviders;
using Football.Core.Persistence.MySql;
using Football.Core.Persistence.MySql.Contexts;
using Football.Services.GameService.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Football.Services.GameService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 28));

            services.AddDbContext<FootballDbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("FootballDbContext"), mySqlServerVersion));

            services.AddScoped<IFootballDataProvider, MySqlFootballDataProvider>();
            services.AddSignalR();
            services.AddControllers();
            services.AddHealthChecks();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service.Game", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service.Game v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.WithOrigins(Configuration["AllowedOrigin"])
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST")
                    .AllowCredentials();
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health");
                endpoints.MapHub<GameHub>("/hub/football/game");
                endpoints.MapControllers();
            });
        }
    }
}
