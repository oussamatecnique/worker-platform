using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using worker.platform.application.BackOffice.Repositories;
using worker.platform.application.Common;
using worker.platform.application.Common.Caching;
using worker.platform.application.JobsDeals.Repositories;
using worker.platform.application.Users.Repositories;
using worker.platform.infrastructure.Caching;
using worker.platform.infrastructure.Common;
using worker.platform.infrastructure.JobsEntities;
using worker.platform.infrastructure.TaskType;
using worker.platform.infrastructure.Users;

namespace worker.platform.infrastructure;

public static class DependencyInjection
{
    public static void ConfigureInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        var appConnectionString = configuration.GetConnectionString("Application");
        services.AddDbContext<ApplicationDbContext>(c => c.UseSqlServer(appConnectionString));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkerRepository, WorkerRepository>();
        services.AddScoped<IJobRequestRepository, JobRequestRepository>();
        services.AddScoped<IJobResponseRepository, JobResponseRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITaskTypeParamDefinitionRepository, ITaskTypeParamsDefinitionRepository>();
        services.AddScoped<ICacheRepository, CacheRepository>();


        services.AddScoped<ITaskTypeRepository, TaskTypeRepository>();
    }
}
