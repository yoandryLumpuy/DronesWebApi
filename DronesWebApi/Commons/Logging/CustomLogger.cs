using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace DronesWebApi.Commons.Logging
{
    public sealed class CustomLogger : ILogger
    {
        private readonly string _name;
        private readonly Func<CustomLoggerConfiguration> _getCurrentConfig;

        public CustomLogger(string name, Func<CustomLoggerConfiguration> getCurrentConfig) => (_name, _getCurrentConfig) = (name, getCurrentConfig);

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        public bool IsEnabled(LogLevel logLevel) => _getCurrentConfig().LogLevelToFileName.ContainsKey(logLevel);

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var config = _getCurrentConfig();

            File.AppendAllLines(config.LogLevelToFileName[logLevel], new List<string>()
            {
                Environment.NewLine,
                $"[{eventId.Id,2}: {logLevel,-12}]",
                $"{formatter(state, exception)}"
            });
        }
    }
}
