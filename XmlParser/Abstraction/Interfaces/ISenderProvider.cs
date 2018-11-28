using System.Collections.Generic;
using System.IO;

namespace XmlParser.Abstraction.Interfaces
{
    /// <summary>
    /// Provide logic of sending content.
    /// </summary>
    /// <typeparam name="T">Type of content (data).</typeparam>
    public interface ISenderProvider<in T>
    {
        /// <summary>
        /// Logic of sending content.
        /// </summary>
        /// <param name="data">Content to send.</param>
        /// <param name="destinationStream">Destination to send.</param>
        void Send(IEnumerable<T> data, Stream destinationStream);
    }
}
