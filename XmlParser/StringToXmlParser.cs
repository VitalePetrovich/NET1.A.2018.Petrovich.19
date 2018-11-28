using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using XmlParser.Abstraction;
using XmlParser.Abstraction.Interfaces;
using XmlParser.Logger;

namespace XmlParser
{
    /// <summary>
    /// Class for parsing string to xml-doc.
    /// </summary>
    public sealed class StringToXmlParser : Parser<string, XElement>
    {
        public StringToXmlParser(
            IDataProvider<string> dataProvider,
            IParserProvider<string, XElement> parserProvider,
            ISenderProvider<XElement> senderProvider,
            ILogger logger)
            : base(dataProvider, parserProvider, senderProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(logger);
            _logger.Register(this);
        }

        public event EventHandler<LoggerInfoEventArgs> ParsignEvent = delegate { };

        public event EventHandler<LoggerInfoEventArgs> ParsingError = delegate { };

        private readonly ILogger _logger;

        /// <summary>
        /// Parse content of source file and save to destination file.
        /// </summary>
        /// <param name="sourcePath">Source file path.</param>
        /// <param name="destinationPath">Destination file path.</param>
        /// <exception cref="ArgumentNullException">Throws if incomming parameters are null.</exception>
        /// <exception cref="FileNotFoundException">Throws if source file is not found.</exception>
        /// <returns>Count of parsed lines.</returns>
        public override int Parse(string sourcePath, string destinationPath)
        {
            if (string.IsNullOrEmpty(sourcePath))
                throw new ArgumentNullException(nameof(sourcePath));

            if (!File.Exists(sourcePath))
                throw new FileNotFoundException($"File not found", sourcePath);

            if (string.IsNullOrEmpty(destinationPath))
                throw new ArgumentNullException(nameof(destinationPath));

            int countOfParsedLines = 0;
            
            OnStartParsing(sourcePath);

            using (var sourceStream = File.OpenRead(sourcePath))
            using (var destinationStream = File.OpenWrite(destinationPath))
            {
                IEnumerable<string> data = _dataProvider.GetData(sourceStream);

                IEnumerable<XElement> parsedData = Parsing(data);
            
                _senderProvider.Send(parsedData, destinationStream);
            }

            OnEndParsing(sourcePath, destinationPath, countOfParsedLines);

            return countOfParsedLines;
            
            IEnumerable<XElement> Parsing(IEnumerable<string> d)
            {
                int currentLine = -1;
                
                foreach (var item in d)
                {
                    currentLine++;

                    //Наверное стоило бы выделить интерфейс для валидатора и передавать его как параметр.
                    //Но не уверен стоит ли в данном случае.
                    try
                    {
                        new Uri(item);
                    }
                    catch (UriFormatException ex)
                    { 
                        OnParsingError(currentLine);
                        continue;
                    }

                    yield return _parserProvider.Parse(item);

                    countOfParsedLines++;
                }
            }
        }

        private void OnStartParsing(string sourcePath)
        {
            ParsignEvent(this, new LoggerInfoEventArgs() {
                                                             Message = $"Start parsing { sourcePath }.",
                                                             Time = DateTime.Now
                                                         });
        }

        private void OnEndParsing(string sourcePath, string destinationPath, int countOfParsedLines)
        {
            ParsignEvent(this, new LoggerInfoEventArgs() { Message = $"Parsing {sourcePath} has been finished. " +
                                                                     $"Total lines parsed: {countOfParsedLines}. " +
                                                                     $"File has been saved to {destinationPath}. ",
                                                           Time = DateTime.Now
                                                          });
        }

        private void OnParsingError(int currentLine)
        {
            ParsingError(this, new LoggerInfoEventArgs()
                                   {
                                       Message = $"Parsing error in line", 
                                       Time = DateTime.Now,
                                       NumberOfLine = currentLine
                                   });
        }
    }
}
