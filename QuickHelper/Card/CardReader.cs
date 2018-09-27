using System.Drawing.Printing;
using System.IO;
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
        private readonly FileCardParserFactory _cardParserFactory;


        public CardReader(IAppConfig appConfig,
            ILogger logger,
            ICardSetRepository cardSetRepository,
            IFileWatcher fileWatcher,
            FileCardParserFactory cardParserFactory)
        {
            _appConfig = appConfig;
            _logger = logger;
            _cardSetRepository = cardSetRepository;
            _fileWatcher = fileWatcher;
            _cardParserFactory = cardParserFactory;
        }

        public void ReadAllFiles()
        {
            if (_appConfig == null)
                return;

            var newFiles = _fileWatcher.GetAnkiFilePathList();

            foreach (string filePath in newFiles)
            {
                var content = File.ReadAllLines(filePath);

                var parser = _cardParserFactory.GetParser(filePath, content);

                var cardSet = parser.Parse(filePath, content);

                _cardSetRepository.Add(cardSet);
            }
        }
    }
}