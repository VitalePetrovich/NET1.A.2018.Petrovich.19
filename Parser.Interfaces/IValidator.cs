namespace Parser.Interfaces
{
    /// <summary>
    /// Provide logic of data validation. 
    /// </summary>
    public interface IValidator<in TSource>
    {
        /// <summary>
        /// Validation of data.
        /// </summary>
        /// <param name="data">Data to validate.</param>
        /// <returns>TRUE: if data is valid.</returns>
        bool Validate(TSource data);
    } 
}
