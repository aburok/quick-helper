using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuickHelper.Card
{
    public class FileCardParserFactory
    {
        private readonly IEnumerable<IFileCardParser> _parsers;

        public FileCardParserFactory(IEnumerable<IFileCardParser> parsers)
        {
            _parsers = parsers;
        }

        public IFileCardParser GetParser(string filePath, IEnumerable<string> fileContent)
        {
            if (!fileContent.Any())
                return EmptyCardParser.Empty;

            var firstMatchingParser = _parsers.FirstOrDefault(parser => parser.CanParse(filePath, fileContent));
            if (firstMatchingParser != null)
            {
                return firstMatchingParser;
            }

            return EmptyCardParser.Empty;
        }
    }
}