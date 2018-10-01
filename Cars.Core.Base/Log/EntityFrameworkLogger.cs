using Microsoft.Extensions.Logging;
using System;

namespace Cars.Core.Base.Log
{
    public class EntityFrameworkLogger : ILogger, ILoggerProvider
    {
        private NLog.Logger logger;

        public void Dispose()
        {
        }

        private NLog.LogLevel GetNLogLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Critical: return NLog.LogLevel.Fatal;
                case LogLevel.Debug: return NLog.LogLevel.Debug;
                case LogLevel.Error: return NLog.LogLevel.Error;
                case LogLevel.Information: return NLog.LogLevel.Info;
                case LogLevel.Trace: return NLog.LogLevel.Trace;
                case LogLevel.Warning: return NLog.LogLevel.Warn;
                default: return NLog.LogLevel.Off;
            }
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (logger != null)
            {
                var nlogLevel = GetNLogLevel(logLevel);
                string logText = formatter(state, exception);
                logger.Log(nlogLevel, logText);
            }
        }

        bool ILogger.IsEnabled(LogLevel logLevel)
        {
            if (logger == null)
            {
                return false;
            }
            var nlogLevel = GetNLogLevel(logLevel);
            return logger.IsEnabled(nlogLevel);
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            return null;
        }

        ILogger ILoggerProvider.CreateLogger(string categoryName)
        {
            try
            {
                var logger = new EntityFrameworkLogger { logger = NLog.LogManager.GetCurrentClassLogger() };
                return logger;
            }
            catch
            {
                // return new ConsoleLogger(); // you must implement this
                return null;
            }
        }
    }
}
