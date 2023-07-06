using Microsoft.Extensions.Logging;

namespace DronesWebApi.Commons.Logging
{
    public class CustomLoggingProvider: ILoggerProvider
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public ILogger CreateLogger(string categoryName)
        {
            throw new System.NotImplementedException();
        }
    }
}
