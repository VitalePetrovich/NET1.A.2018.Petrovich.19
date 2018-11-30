using System;
using System.Collections.Generic;
using System.IO;
using Parser.Interfaces;

namespace Parser
{
    /// <summary>
    /// Class-provider data for parsing.
    /// </summary>
    public class FileStringsDataProvider : IDataProvider<string>
    {
        private readonly string sourcePath;

        public FileStringsDataProvider(string sourcePath)
        {
            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentNullException(nameof(sourcePath));
            }

            this.sourcePath = sourcePath;
        }
        
        /// <summary>
        /// Provide collection of strings from data source.
        /// </summary>
        /// <exception cref="FileNotFoundException">Throws if file is not found.</exception>
        /// <returns>Collection of strings.</returns>
        public IEnumerable<string> GetData()
        {
            if (!File.Exists(this.sourcePath))
            {
                throw new FileNotFoundException($"File: {this.sourcePath} not found.");
            }

            using (StreamReader sr = new StreamReader(this.sourcePath))
            {
                while (!sr.EndOfStream)
                {
                    yield return sr.ReadLine();
                }
            }
        }
    }
}
