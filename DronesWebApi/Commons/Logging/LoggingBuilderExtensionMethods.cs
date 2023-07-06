using System;
using FluentValidation.Validators;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace DronesWebApi.Commons.Logging
{
    public static class LoggingBuilderExtensionMethods
    {
        public static ILoggingBuilder AddCustomLogging(this ILoggingBuilder builder)
        {
            //todo: Implement here dependency injection for custom logger provider

            //builder.AddConfiguration();
            //builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggingProvider>());

            return builder;
        }
    }
}
