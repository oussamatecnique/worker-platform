using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Validot;
using worker.platform.application.BackOffice;
using worker.platform.application.Users.Mappers;
using worker.platform.application.Common.Auth;
using worker.platform.application.Common.Models;
using worker.platform.application.Common.Validation;
using worker.platform.application.JobsDeals.Mappers;
using worker.platform.application.JobsDeals.Services;
using worker.platform.application.JobsDeals.Validators;
using worker.platform.application.Users.Mappers;
using worker.platform.application.Users.Services;
using worker.platform.application.Users.Validators;
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

    public static void AddValidators(this IServiceCollection services)
    {
        var holderAssemblies = Assembly.GetExecutingAssembly();
        var holders = Validator.Factory.FetchHolders(holderAssemblies)
            .GroupBy(h => h.SpecifiedType)
            .Select(s => new
            {
                ValidatorType = s.First().ValidatorType,
                ValidatorInstance = s.First().CreateValidator()
            });
        foreach (var holder in holders)
        {
            services.AddSingleton(holder.ValidatorType, holder.ValidatorInstance);
        }

        services.AddScoped<IModelValidator, ModelValidator>();
        services.AddScoped<IJobAssignmentValidator, JobAssignmentValidator>();
    }
}
