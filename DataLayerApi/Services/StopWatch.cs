using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerApi.Services
{
    /// <summary>
    /// Stopky pro logování časů pomocí using
    /// </summary>
    public class StopWatch : IDisposable
    {
        private readonly ILogger logger;
        private readonly LogLevel logLevel;
        private readonly string identMessage;
        private DateTime startDate = DateTime.Now;

        public StopWatch(ILogger logger, LogLevel logLevel, string identMessage)
        {
            this.logger = logger;
            this.logLevel = logLevel;
            this.identMessage = identMessage;
        }

        public void SplitTime(string identMessage)
        {
            this.Log(this.logLevel, $"{nameof(SplitTime)}-> {identMessage}");
        }

        public void SplitTime(LogLevel logLevel, string identMessage)
        {
            this.Log(logLevel, $"{nameof(SplitTime)}-> {identMessage}");
        }

        public void SplitTimeAndReset(string identMessage)
        {
            this.Log(this.logLevel, $"{nameof(SplitTimeAndReset)}-> {identMessage}");
            this.startDate = DateTime.Now;
        }

        public void SplitTimeAndReset(LogLevel logLevel, string identMessage)
        {
            this.Log(logLevel, $"{nameof(SplitTimeAndReset)}-> {identMessage}");
            this.startDate = DateTime.Now;
        }

        private void Log(LogLevel logLevel, string message)
        {
            this.logger.Log(logLevel, $"{message} -> {DateTime.Now - this.startDate}");
        }

        public void Dispose()
        {
            this.Log(this.logLevel, this.identMessage);
        }
    }
}
