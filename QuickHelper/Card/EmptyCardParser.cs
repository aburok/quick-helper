using System.Collections.Generic;
using System.Linq;

namespace QuickHelper.Card
{
    public class EmptyCardParser : IFileCardParser
    {
        public static EmptyCardParser Empty = new EmptyCardParser();

        public bool CanParse(string fileName, IEnumerable<string> content)
        {
            return true;
        }

        public CardSetModel Parse(string filePath, IEnumerable<string> content)
        {
            return CardSetModel.Empty;
        }
    }
}