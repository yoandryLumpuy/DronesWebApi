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

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient(serviceType: typeof(IPipelineBehavior<,>), implementationType: typeof(ValidationBehaviour<,>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(serviceType: typeof(IRepository<>), implementationType: typeof(Repository<>));

            return services;
        }
    }
}
