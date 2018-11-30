using System;
using Parser.Interfaces;

namespace Parser
{
    /// <summary>
    /// Class contain logic of parsing string url to xml-element.
    /// </summary>
    public class ToUriParse : IParserProvider<string, Uri>
    {
        /// <summary>
        /// Convert string url to uri type.
        /// </summary>
        /// <param name="data">String with url.</param>
        /// <exception cref="ArgumentNullException">Throws if incoming string is null or empty.</exception>
        /// <returns>Uri-element.</returns>
        public Uri Parse(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data));
            }
            
            return new Uri(data);
        }
    }
}
