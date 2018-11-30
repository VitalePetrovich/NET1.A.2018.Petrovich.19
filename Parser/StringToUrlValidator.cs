using System;
using System.Linq;
using Parser.Interfaces;

namespace Parser
{
    public class StringToUrlValidator : IValidator<string>
    {
        public bool Validate(string data)
        {
            try
            {
                Uri uri = new Uri(data);

                if (string.IsNullOrEmpty(uri.Query))
                {
                    return true;
                }

                return uri.Query.Substring(1)
                    .Split('&')
                    .All(p => p.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries).Length == 2);
            }
            catch (UriFormatException ex)
            {
                return false;
            }
        }
    }
}
