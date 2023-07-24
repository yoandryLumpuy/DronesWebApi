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
        public static ILoggingBuilder AddCustomLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggingProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <CustomLoggerConfiguration, CustomLoggingProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder, Action<CustomLoggerConfiguration> configure)
        {
            builder.AddCustomLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
