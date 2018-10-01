using Cars.SharedKernel.Log;
using System;

namespace Cars.Core.Base.Log
{
    public class AppLogger : IAppLogger
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public void Trace(string message)
        {
            logger.Trace(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Warn(Exception exception)
        {
            Warn(exception.ToString());
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(Exception exception)
        {
            Error(exception.ToString());
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(Exception exception)
        {
            Fatal(exception.ToString());
        }
    }
}
