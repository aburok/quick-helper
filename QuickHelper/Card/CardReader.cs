using System;
using System.IO;
using Newtonsoft.Json;
using QuickHelper.Common;
using QuickHelper.Configuration;
using QuickHelper.Files;
using QuickHelper.Repository;

namespace QuickHelper.Card
{
    public class CardReader
    {
        private readonly IAppConfig _appConfig;
        private readonly ILogger _logger;
        private readonly ICardSetRepository _cardSetRepository;
        private readonly IFileWatcher _fileWatcher;


        public CardReader(IAppConfig appConfig,
            ILogger logger,
            ICardSetRepository cardSetRepository,
            IFileWatcher fileWatcher)
        {
            _appConfig = appConfig;
            _logger = logger;
            _cardSetRepository = cardSetRepository;
            _fileWatcher = fileWatcher;
        }

        public void ReadAllFiles()
        {
            if (_appConfig == null)
                return;

            var newFiles = _fileWatcher.GetAnkiFilePathList();

            foreach (string filePath in newFiles)
            {
                var cardSet = ReadCard(filePath);
                _cardSetRepository.Add(cardSet);
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