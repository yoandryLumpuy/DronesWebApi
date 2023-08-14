using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace DronesWebApi.Persistence.Interceptors
{
    public class PerformanceInterceptor: DbCommandInterceptor
    {
        private readonly ILogger<PerformanceInterceptor> _logger;
        private const int MillisecondsThreshold = 200;

        public PerformanceInterceptor(ILogger<PerformanceInterceptor> logger)
        {
            _logger = logger;
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            if (eventData.Duration.TotalMilliseconds > MillisecondsThreshold)
                _logger?.LogInformation($"Query execution time was exceeded. {eventData.Command.CommandText}");

            return base.ReaderExecuted(command, eventData, result);
        }
    }
}
