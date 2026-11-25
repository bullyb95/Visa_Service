using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi;
using VisaService.Application.Services.Mapping;
using VisaService.Infrastructure.EntityFramework;
using VisaService.WebHost.Helpers;
using VisaService.WebHost.Mapping;
using VisaService.WebHost.Middlewares;

namespace VisaService.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Строка подключения: appsettings.Development.json или переменная окружения
            // ConnectionStrings__ApplicationDbContext (переменная окружения имеет приоритет).
            var dbConnectionString = builder.Configuration.GetConnectionString(nameof(ApplicationDbContext));

            if (string.IsNullOrEmpty(dbConnectionString))
            {
                throw new InvalidOperationException("Connection string for ApplicationDbContext is not configured.");
            }

            // Infrastructure: DbContext + репозитории
            builder.Services.AddInfrastructure(dbConnectionString);

            // Application: сервисы use cases
            builder.Services.AddApplicationServices();

            builder.Services.AddAutoMapper(typeof(PresentationProfile), typeof(ApplicationProfile));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Visa Service API",
                    Description = "API сервиса выдачи виз: регистрация заявителей и визовых офицеров, " +
                                  "создание заявок и их рассмотрение (принять, проверить, выдать визу / отказать)."
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddHealthChecks()
                            .AddCheck<DatabaseHealthCheck>("database");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.MapHealthChecks("/health");

            app.MigrateDatabase<ApplicationDbContext>();

            app.Run();
        }
    }
}
