using VisaService.Application.Services;
using VisaService.Application.Services.Abstractions;
using VisaService.Domain.Repositories.Abstractions;
using VisaService.Infrastructure.EntityFramework;
using VisaService.Infrastructure.Repositories.Implementations.EF;

namespace VisaService.WebHost.Helpers;

/// <summary>DI-регистрация слоёв. Вынесено из Program.cs, чтобы он не разрастался.</summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnectionString)
    {
        services.AddNpgsql<ApplicationDbContext>(dbConnectionString, options =>
        {
            options.MigrationsAssembly("VisaService.Infrastructure.EntityFramework");
        });

        services.AddScoped<IApplicantRepository, EfApplicantRepository>();
        services.AddScoped<IOfficerRepository, EfOfficerRepository>();
        services.AddScoped<IVisaApplicationRepository, EfVisaApplicationRepository>();

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicantsApplicationService, ApplicantsApplicationService>();
        services.AddScoped<IOfficersApplicationService, OfficersApplicationService>();
        services.AddScoped<IVisaApplicationsApplicationService, VisaApplicationsApplicationService>();
        services.AddScoped<IApplicantActionsApplicationService, ApplicantActionsApplicationService>();
        services.AddScoped<IOfficerActionsApplicationService, OfficerActionsApplicationService>();

        return services;
    }
}
