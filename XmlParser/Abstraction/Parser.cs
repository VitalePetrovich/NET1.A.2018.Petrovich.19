using XmlParser.Abstraction.Interfaces;

namespace XmlParser.Abstraction
{
    using System;

    /// <summary>
    /// Abstract base class for parsers.
    /// </summary>
    /// <typeparam name="TSource">Source content type.</typeparam>
    /// <typeparam name="TResult">Result content tyoe.</typeparam>
    public abstract class Parser<TSource, TResult>
    {
        protected Parser(
            IDataProvider<TSource> dataProvider,
            IParserProvider<TSource, TResult> parserProvider,
            ISenderProvider<TResult> senderProvider)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            _parserProvider = parserProvider ?? throw new ArgumentNullException(nameof(parserProvider));
            _senderProvider = senderProvider ?? throw new ArgumentNullException(nameof(senderProvider));
        }

        protected readonly IDataProvider<TSource> _dataProvider;

        protected readonly IParserProvider<TSource, TResult> _parserProvider;

        protected readonly ISenderProvider<TResult> _senderProvider;

        /// <summary>
        /// Parse content of source file and save to destination file.
        /// </summary>
        /// <param name="sourcePath">Source file path.</param>
        /// <param name="destinationPath">Destination file path.</param>
        /// <returns>Count of parsed lines.</returns>
        public abstract int Parse(string sourcePath, string destinationPath);
        //Парсинг из потока в поток?
    }
}
