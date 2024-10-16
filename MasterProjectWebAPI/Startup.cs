using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MasterProjectCommonUtility.Configuration;
using NLog;
using MasterProjectWebAPI.Helper;
using MasterProjectWebAPI.AutoMapperProfile;

namespace MasterProjectWebAPI
{
    public class Startup
    {
        private AppsettingsConfig? _appSettings;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            string path = string.Concat(Directory.GetCurrentDirectory(), "/nlog.config");
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"),true);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();
            services.AddAutoMapper(typeof(MapperProfileDeclaration));
            var serviceRegistry = new ServiceRegistry();
            services.AddMvc();

            _appSettings = LoadConfiguration(services);
            serviceRegistry.ConfigureDataContext(services, _appSettings);
            serviceRegistry.ConfigureDependencies(services, _appSettings);
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "MasterProject_WEB_API_V1.0",
            //        Version = "v1"
            //    });
            //});


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MasterProject_WEB_API_V1.0",
                    Version = "v1"
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(
                options => options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();


            // Add Swagger middleware before endpoints
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("My Running Environment: " + env.EnvironmentName + "");
                });
            });
        }

        private AppsettingsConfig LoadConfiguration(IServiceCollection services)
        {
            AppsettingsConfig appSettings = new AppsettingsConfig();
            Configuration.Bind(appSettings);
            services.AddSingleton(appSettings);
            return appSettings;
        }

    }
}
