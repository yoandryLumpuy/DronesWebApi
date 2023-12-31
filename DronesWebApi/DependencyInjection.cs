﻿using DronesWebApi.Commons.Behaviors;
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
using DronesWebApi.Commons.Models;
using DronesWebApi.Infrastructure.FileManager;
using DronesWebApi.Persistence.Interceptors;
using Microsoft.Extensions.Logging;

namespace DronesWebApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddFilesManagement(configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidation();

            services.AddMediator();

            services.AddBackgroundServices(configuration);

            return services;
        }

        private static void AddFilesManagement(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<UploadFileOptions>().Bind(configuration.GetSection(nameof(UploadFileOptions)));

            services.AddScoped<IFileManager, FileManager>();
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

        private static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<PerformanceInterceptor>();

            services.AddDbContext<DronesContext>((serviceProvider, dbContextOptionBuilder) =>
            {
                dbContextOptionBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"))
                    .AddInterceptors(serviceProvider.GetRequiredService<PerformanceInterceptor>());
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(serviceType: typeof(IRepository<>), implementationType: typeof(Repository<>));
        }
    }
}
