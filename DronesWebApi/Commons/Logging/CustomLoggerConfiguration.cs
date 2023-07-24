using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace DronesWebApi.Commons.Logging
{
    public class CustomLoggerConfiguration
    {
        public Dictionary<LogLevel, string> LogLevelToFileName { get; set; } = new()
        {
            [LogLevel.Information] = "InformationLogs.txt",
            [LogLevel.Error] = "ErrorLogs.txt"
        };
    }
}
