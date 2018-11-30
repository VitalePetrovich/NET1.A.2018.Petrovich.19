using System;
using System.Collections.Generic;
using Ninject;
using Parser.Interfaces;

namespace Parser
{
    /// <summary>
    /// Class for parsing string to xml-doc.
    /// </summary>
    public class Parser
    {
        private readonly IDataProvider<string> dataProvider;

        private readonly IParserProvider<string, Uri> parserProvider;

        private readonly ISenderProvider<Uri> senderProvider;

        private readonly IValidator<string> validator;
        
        public Parser(
            IDataProvider<string> dataProvider,
            IParserProvider<string, Uri> parserProvider,
            ISenderProvider<Uri> senderProvider,
            IValidator<string> validator)
        {

            this.dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            this.parserProvider = parserProvider ?? throw new ArgumentNullException(nameof(parserProvider));
            this.senderProvider = senderProvider ?? throw new ArgumentNullException(nameof(senderProvider));
            this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [Inject]
        public ILogger Logger { get; set; }

        /// <summary>
        /// Parse content of source file and save to destination file.
        /// </summary>
        /// <returns>Count of parsed lines.</returns>
        public int Parse()
        {
            int countOfParsedLines = 0;
            
            this.Logger?.Info("Start parsing.");

            IEnumerable<string> data = this.dataProvider.GetData();

            IEnumerable<Uri> parsedData = Parsing(data);
            
            this.senderProvider.Send(parsedData);
            
            this.Logger?.Info("Parsing finished. " + 
                              $"Total lines parsed: {countOfParsedLines}");

            return countOfParsedLines;
            
            IEnumerable<Uri> Parsing(IEnumerable<string> d)
            {
                int currentLine = -1;
                
                foreach (var item in d)
                {
                    currentLine++;

                    if (!this.validator.Validate(item))
                    {
                        this.Logger?.Info($"Parsing error at line {currentLine}.");
                        continue;
                    }

                    yield return this.parserProvider.Parse(item);

                    countOfParsedLines++;
                }
            }
        }
    }
}
