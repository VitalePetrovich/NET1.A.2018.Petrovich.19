using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using XmlParser.Abstraction.Interfaces;

namespace XmlParser
{
    /// <summary>
    /// Save xml-elemens as entire xml-file.
    /// </summary>
    public class XmlFileSaver : ISenderProvider<XElement>
    {
        /// <summary>
        /// Save data to stream.
        /// </summary>
        /// <param name="data">Data for saving.</param>
        /// <param name="destinationStream">Stream to save.</param>
        /// <exception cref="ArgumentNullException">Throws if data or destination stream are null.</exception>
        public void Send(IEnumerable<XElement> data, Stream destinationStream)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (destinationStream == null)
                throw new ArgumentNullException(nameof(destinationStream));

            new XElement("urlAddresses", data).Save(destinationStream);
        }
    }
}
