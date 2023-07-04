using DronesWebApi.Commons.Behaviors;
using DronesWebApi.Core;
using DronesWebApi.Core.Repositories;
using DronesWebApi.Persistence;
using DronesWebApi.Persistence.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DronesWebApi.Commons.Configuration;
using DronesWebApi.HostedServices;
using DronesWebApi.Commons.Middlewares;

namespace DronesWebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DronesContext>(dbContextOptionBuilder =>
            {
                dbContextOptionBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidation();

            services.AddMediator();

            services.AddPersistence();

            services.AddBackgroundServices(configuration);

            return services;
        }

        private static void AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddTransient(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(ValidationBehaviour<,>));
        }

        private static void AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<BackgroundServicesOptions>().Bind(configuration.GetSection(nameof(BackgroundServicesOptions)));

            services.AddHostedService<DroneBatteryLevelCheckerBackgroundService>();
        }

        private static void AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(serviceType: typeof(IRepository<>), implementationType: typeof(Repository<>));
        }
    }
}
