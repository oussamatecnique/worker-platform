using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using worker.platform.application.BackOffice;
using worker.platform.application.Users.Mappers;
using worker.platform.application.Common.Auth;
using worker.platform.application.Common.Models;
using worker.platform.application.JobsDeals.Mappers;
using worker.platform.application.JobsDeals.Services;
using worker.platform.application.JobsDeals.Validators;
using worker.platform.application.JobsDeals.Validators.Global;
using worker.platform.application.Users.Services;
using UserMapper = worker.platform.application.Users.Mappers.UserMapper;

namespace worker.platform.application;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<IUserMapper, UserMapper>();
        services.AddSingleton<IJobAssignmentMapper, JobAssignmentMapper>();
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJwtHelper, JwtHelper>();
        services.AddScoped<IJobAssignmentService, JobAssignmentService>();
        services.AddScoped<IBoService, BoService>();

        services.AddCacheStack(x => x.AddMemoryCacheLayer());
        services.AddValidators();

        return services;
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AddJobRequestValidator>();
        
        services.AddScoped<IJobAssignmentValidator, JobAssignmentValidator>();
    }
}
