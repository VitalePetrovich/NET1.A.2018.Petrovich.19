using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Parser.Interfaces;

namespace Parser
{
    /// <summary>
    /// Save xml-elemens as entire xml-file.
    /// </summary>
    public class XmlFileSaver : ISenderProvider<Uri>
    {
        private readonly string destinationPath;

        public XmlFileSaver(string destinationPath)
        {
            if (string.IsNullOrEmpty(destinationPath))
            {
                throw new ArgumentNullException(nameof(destinationPath));
            }

            this.destinationPath = destinationPath;
        }

        /// <summary>
        /// Save data to stream.
        /// </summary>
        /// <param name="data">Data for saving.</param>
        /// <exception cref="ArgumentNullException">Throws if data or destination stream are null.</exception>
        public void Send(IEnumerable<Uri> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            IEnumerable<XElement> elements = data.Select(
                d => new XElement("urlAdress", 
                        new XElement("host", 
                            new XAttribute("name", d.Host)),
                        (!d.Segments.Any(s => s != "/")) ? null : new XElement("uri", 
                                                                    d.AbsolutePath
                                                                        .Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries)
                                                                        .Select(s => new XElement("segment", s))),
                        (string.Empty == d.Query) ? null : new XElement("parameters", 
                                                            d.Query.Substring(1)
                                                                .Split('&')
                                                                .Select(p => new XElement("parameter", GetAttributes(p))))));

            IEnumerable<XAttribute> GetAttributes(string attributePair)
            {
                string[] parameters = attributePair.Split('=');

                yield return new XAttribute("key", parameters[0]);
                yield return new XAttribute("value", parameters[1]);
            }

            using (var destinationStream = new FileStream(this.destinationPath, FileMode.Create))
            {
                new XElement("urlAddresses", elements).Save(destinationStream);
            }
        }
    }
}
