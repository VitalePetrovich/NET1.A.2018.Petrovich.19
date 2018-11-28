using System;
using System.Collections.Generic;
using System.IO;
using XmlParser.Abstraction.Interfaces;

namespace XmlParser
{
    /// <summary>
    /// Class-provider data for parsing.
    /// </summary>
    public class FileStringsDataProvider : IDataProvider<string>
    {
        /// <summary>
        /// Provide collection of strings from stream.
        /// </summary>
        /// <param name="sourceStream">Stream for reading strings.</param>
        /// <exception cref="ArgumentNullException">Throws if stream is null.</exception>
        /// <returns>Collection of strings.</returns>
        public IEnumerable<string> GetData(Stream sourceStream)
        {
            if (sourceStream == null)
                throw new ArgumentNullException(nameof(sourceStream));
            
            using (StreamReader sr = new StreamReader(sourceStream))
            {
                while (!sr.EndOfStream)
                {
                    yield return sr.ReadLine();
                }
            }
        }
    }
}
