using System;

namespace XmlParser.Logger
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
            _logger.Fatal(ex, ex.Message);
        }

        /// <summary>
        /// Info message.
        /// </summary>
        /// <param name="msg">Message.</param>
        public void Info(string msg)
        {
            _logger.Info(msg);
        }

        /// <summary>
        /// Warning message. If exception occurs, but process is can be still run.
        /// </summary>
        /// <param name="msg">Message.</param>
        /// <param name="ex">Exception.</param>
        public void Warning(string msg, Exception ex)
        {
            _logger.Warn($"{msg} ({ex.Message})");
        }

        /// <summary>
        /// Register logger to parser events.
        /// </summary>
        /// <param name="printer">Printer.</param>
        public void Register(StringToXmlParser parser)
        {
            parser.ParsignEvent += HandleParsingEvent;
            parser.ParsingError += HandleParsingError;
        }

        /// <summary>
        /// Unregister logger to parser events.
        /// </summary>
        /// <param name="printer">Printer.</param>
        public void Unregister(StringToXmlParser parser)
        {
            parser.ParsignEvent -= HandleParsingEvent;
            parser.ParsingError -= HandleParsingError;
        }
        
        private void HandleParsingEvent(object sender, LoggerInfoEventArgs info)
        {
            Info($"[{info.Time}] {info.Message}");
        }

        private void HandleParsingError(object sender, LoggerInfoEventArgs info)
        {
            Info($"[{info.Time}] {info.Message} {info.NumberOfLine}");
        }
    }
}
