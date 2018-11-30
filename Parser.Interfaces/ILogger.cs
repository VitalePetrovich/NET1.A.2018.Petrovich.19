using System;

namespace Parser.Interfaces
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
    }
}
