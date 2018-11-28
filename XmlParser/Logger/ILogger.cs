using System;

namespace XmlParser.Logger
{
    public interface ILogger
    {
        /// <summary>
        /// Info message.
        /// </summary>
        /// <param name="msg">Message.</param>
        void Info(string msg);

        /// <summary>
        /// Warning message. If exception occurs, but process is can be still run.
        /// </summary>
        /// <param name="msg">Message.</param>
        /// <param name="ex">Exception.</param>
        void Warning(string msg, Exception ex);

        /// <summary>
        /// Fatal message. If critical exception uccurs.
        /// </summary>
        /// <param name="ex">Exception.</param>
        void Fatal(Exception ex);

        /// <summary>
        /// Register logger to parser events.
        /// </summary>
        /// <param name="printer">Printer.</param>
        void Register(StringToXmlParser parser);

        /// <summary>
        /// Unregister logger to parser events.
        /// </summary>
        /// <param name="printer">Printer.</param>
        void Unregister(StringToXmlParser parser);
    }
}
