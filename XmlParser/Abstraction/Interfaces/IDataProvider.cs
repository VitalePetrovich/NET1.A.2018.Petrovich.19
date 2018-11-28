using System.Collections.Generic;
using System.IO;

namespace XmlParser.Abstraction.Interfaces
{
    /// <summary>
    /// Provide logic of getting data for parsing.
    /// </summary>
    /// <typeparam name="T">Type of parsing content.</typeparam>
    public interface IDataProvider<out T>
    {
        /// <summary>
        /// Logic of getting data.
        /// </summary>
        /// <param name="sourceStream">Source stream.</param>
        /// <returns>Collection of data.</returns>
        IEnumerable<T> GetData(Stream sourceStream);
    }
}
