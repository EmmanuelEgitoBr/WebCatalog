
namespace WebCatalog.API.Logging
{
    public class CustomerLogger : ILogger
    {
        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfiguration;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration loggerConfig)
        {
            loggerName = name;
            loggerConfiguration = loggerConfig;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == loggerConfiguration.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string logMessage = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            WriteTextToFile(logMessage);
        }

        private void WriteTextToFile(string message)
        {
            string pathFile = @"C:\Users\e_egi\Downloads\Log_WebCatalog.txt";

            using (StreamWriter streamWriter = new StreamWriter(pathFile, true))
            {
                try
                {
                    streamWriter.WriteLine(message);
                    streamWriter.Close();
                }
                catch (Exception)
                {
                    throw;
                } 
            }
        }
    }
}
