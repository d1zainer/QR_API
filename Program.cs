using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;

namespace QR_API
{
    public class Program
    {
        private const string _commandTest = "http --url=helpful-orca-worthy.ngrok-free.app 5053";
        public static async Task Main(string[] args)
        {
            string desktopPath = Path.Combine(Environment.CurrentDirectory, "ngrok.exe");
            RunExternalApp(desktopPath, _commandTest);

            var builder = WebApplication.CreateBuilder(args);
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
            // Включаем маршрутизацию
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            // Настраиваем маршруты для контроллеров
            app.MapControllers();
             


            // Запускаем приложение
            app.Run();
        }
        private static void RunExternalApp(string filePath, string arguments)
        {
            try
            {
                // Настраиваем процесс для запуска внешнего приложения
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = filePath, // Указанный путь к ngrok.exe
                    Arguments = arguments, // Аргументы для ngrok
                    UseShellExecute = true, // Позволяет запускать приложение в отдельной консоли
                    CreateNoWindow = false // Создает отдельное окно для приложения
                };
                // Запускаем процесс
                Process.Start(processStartInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to run external app: " + ex.Message);
            }
        }
    }
}



