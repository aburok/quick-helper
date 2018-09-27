using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace QuickHelper.Card
{
    class JsonFileCardParser : IFileCardParser
    {
        public CardSetModel Parse(string fileName, IEnumerable<string> content)
        {
            try
            {
                var cardSetFileText = string.Join("", content);
                var cardSet = JsonConvert.DeserializeObject<CardSetModel>(cardSetFileText);
                cardSet.FilePath = fileName;
                cardSet.Init();
                return cardSet;
            }
            catch (Exception e)
            {

            }
            return new CardSetModel();
        }

        public bool CanParse(string fileName, IEnumerable<string> content)
        {
            var extension = Path.GetExtension(fileName);
            return extension.Equals(".json", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}