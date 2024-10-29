using System.Collections.Concurrent;

namespace WebCatalog.API.Logging
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        readonly CustomLoggerProviderConfiguration configuration;
        readonly ConcurrentDictionary<string, CustomerLogger> loggers = 
            new ConcurrentDictionary<string, CustomerLogger>();

        public CustomLoggerProvider(CustomLoggerProviderConfiguration config)
        {
            configuration = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, configuration));
        }

        public void Dispose()
        {
            loggers.Clear();
        }
    }
}
