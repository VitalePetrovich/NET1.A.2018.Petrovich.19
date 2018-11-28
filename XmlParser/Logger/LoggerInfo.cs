using System;

namespace XmlParser.Logger
{
    /// <summary>
    /// Container of logger info.
    /// </summary>
    public class LoggerInfoEventArgs : EventArgs
    {
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public int NumberOfLine { get; set; }
    }
}
