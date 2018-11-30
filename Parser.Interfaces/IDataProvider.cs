using System.Collections.Generic;

namespace Parser.Interfaces
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
        /// <returns>Collection of data.</returns>
        IEnumerable<T> GetData();
    }
}
