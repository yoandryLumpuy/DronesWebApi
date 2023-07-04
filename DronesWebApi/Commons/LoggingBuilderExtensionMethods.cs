using System;
using FluentValidation.Validators;
using Microsoft.Extensions.Logging;

namespace DronesWebApi.Commons
{
    public static class LoggingBuilderExtensionMethods
    {
        public static ILoggingBuilder AddCustomLogging(this ILoggingBuilder builder)
        {
            //TODO: dependency injection of Custom ILoggerProvider goes here  

            return builder;
        }
    }
}
