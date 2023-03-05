using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using Microsoft.OpenApi.Models;
using PikaMLModule.Services;
using System;
using static MLTest.HateSpeechPL;

namespace PikaMLModule
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("Base", builder =>
            {
                builder
                    .WithOrigins("http://localhost:8080", "https://ml.lukas-bownik.net", "https://note.lukas-bownik.net")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PikaMLModule API",
                    Version = "v1.0",
                    Description = "Simple Web API for PikaCloud ML subsystem",
                    Contact = new OpenApiContact
                    {
                        Name = "Arkasian",
                        Email = "lukasbownik99@gmail.com",
                        Url = new Uri("https://me.lukas-bownik.net/")
                    }
                });
            }
            );
            services.AddTransient<PredictionEnginePool<ModelInput, ModelOutput>>();
            services.AddSingleton<IPredictionService, PredictionService>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors("Base");
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PikaMLModule API"));
            app.UseHttpsRedirection();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
