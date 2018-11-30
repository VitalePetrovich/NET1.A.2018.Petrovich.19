using System;
using Parser.Interfaces;

namespace Parser.Logger
{
    /// <summary>
    /// NLog class logger.
    /// </summary>
    public class NLogLogger : ILogger
    {
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Fatal message. If critical exception uccurs.
        /// </summary>
        /// <param name="ex">Exception.</param>
        public void Fatal(Exception ex)
        {
            this._logger.Fatal(ex, ex.Message);
        }

        /// <summary>
        /// Info message.
        /// </summary>
        /// <param name="msg">Message.</param>
        public void Info(string msg)
        {
            this._logger.Info(msg);
        }

        /// <summary>
        /// Warning message. If exception occurs, but process is can be still run.
        /// </summary>
        /// <param name="msg">Message.</param>
        /// <param name="ex">Exception.</param>
        public void Warning(string msg, Exception ex)
        {
            this._logger.Warn($"{msg} ({ex.Message})");
        }
    }
}
