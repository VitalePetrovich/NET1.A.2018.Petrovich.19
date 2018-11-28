using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XmlParser;
using XmlParser.Logger;
using System.Configuration;

namespace XmlParser.Console
{
    using Console = System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            //var sourcePath = ConfigurationManager.AppSettings["sourceFilePath"];
            //var destPath = ConfigurationManager.AppSettings["sourceFilePath"];

            var sourcePath = "SourceUrl.txt";
            var destPath = "OutputXml.xml";

            StringToXmlParser parser = new StringToXmlParser(new FileStringsDataProvider(), new XmlParse(), new XmlFileSaver(), new NLogLogger());
            parser.Parse(sourcePath, destPath);

            Console.WriteLine("All is OK!");

            Console.ReadKey();
        }
    }
}
