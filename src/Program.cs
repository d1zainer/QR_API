using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;

namespace QR_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHealthChecks();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "QR API",
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            // Настройки CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            // Добавляем контроллеры
            builder.Services.AddControllers();
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Включаем использование CORS
            app.UseCors("AllowAllOrigins");
            app.UseHealthChecks("/health");
            // Включаем маршрутизацию
            app.UseRouting();
           
            // Настраиваем маршруты для контроллеров
            app.MapControllers();


            // Запускаем приложение
            app.Run();
        }
    }
}


