namespace XmlParser.Abstraction.Interfaces
{
    /// <summary>
    /// Provide logic of parsing.
    /// </summary>
    /// <typeparam name="TSource">Source content type.</typeparam>
    /// <typeparam name="TResult">Result content type.</typeparam>
    public interface IParserProvider<in TSource, out TResult>
    {
        /// <summary>
        /// Logic of parse source content to result.
        /// </summary>
        /// <param name="data">Source content.</param>
        /// <returns>Result content.</returns>
        TResult Parse(TSource data);
    }
}
