using System;
using System.Xml.Linq;
using XmlParser.Abstraction.Interfaces;

namespace XmlParser
{
    /// <summary>
    /// Class contain logic of parsing string url to xml-element.
    /// </summary>
    public class XmlParse : IParserProvider<string, XElement>
    {
        /// <summary>
        /// Convert string url to xml-element.
        /// </summary>
        /// <param name="data">String with url.</param>
        /// <exception cref="ArgumentNullException">Throws if incoming string is null or empty.</exception>
        /// <returns>xml-element.</returns>
        public XElement Parse(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            Uri uri = new Uri(data);

            XElement uriNode = GetUriNode(uri);

            XElement parametersNode = GetParametersNode(uri);

            return new XElement("urlAddress",
                new XElement("host",
                    new XAttribute("name", uri.Host)),
                uriNode,
                parametersNode);
        }

        private XElement GetUriNode(Uri uri)
        {
            XElement uriNode = new XElement("uri");

            foreach (var segment in uri.AbsolutePath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries))
            {
                uriNode.Add(new XElement("segment", segment));
            }

            return uriNode;
        }

        private XElement GetParametersNode(Uri uri)
        {
            if (string.IsNullOrEmpty(uri.Query))
                return null;

            XElement parametersNode = new XElement("parameters");

            foreach (var parametersPair in uri.Query.Substring(1).Split('&'))
            {
                string[] parameters = parametersPair.Split('=');

                if (parameters.Length != 2)
                    throw new UriFormatException($"Incorrect format parameters pair: {parametersPair}");

                parametersNode.Add(new XElement("parametr", new XAttribute("key", parameters[0]), new XAttribute("value", parameters[1])));
            }

            return parametersNode;
        }
    }
}
