using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using QuickHelper.Common;
using QuickHelper.Configuration;
using QuickHelper.Repository;

namespace QuickHelper.Card
{
    public class CardReader
    {
        private readonly IAppConfig _appConfig;
        private readonly ILogger _logger;
        private readonly ICardSetRepository _cardSetRepository;

        public CardReader(IAppConfig appConfig,
            ILogger logger,
            ICardSetRepository cardSetRepository)
        {
            _appConfig = appConfig;
            _logger = logger;
            _cardSetRepository = cardSetRepository;
        }

        public void ReadAllFiles()
        {
            if (_appConfig == null)
                return;

            if(string.IsNullOrWhiteSpace(_appConfig.SemicolonSeparatedFilePaths))
                return;

            var directories = _appConfig.SemicolonSeparatedFilePaths.Split(';');

            var existingDirectories = directories.Where(Directory.Exists);

            foreach (var dir in existingDirectories)
            {
                var ankiFiles = Directory.EnumerateFiles(
                    dir, 
                    "*.anki.json", 
                    SearchOption.AllDirectories);

                foreach (string filePath in ankiFiles)
                {
                    var cardSet = ReadCard(filePath);
                    _cardSetRepository.Add(cardSet);
                }
            }
        }

        private CardSetModel ReadCard(string path)
        {
            try
            {
                var cardSetFileText = File.ReadAllText(path);
                var cardSet = JsonConvert.DeserializeObject<CardSetModel>(cardSetFileText);
                cardSet.FilePath = path;
                cardSet.Init();
                return cardSet;
            }
            catch (Exception e)
            {
                
            }
            return new CardSetModel();
        } 
    }
}