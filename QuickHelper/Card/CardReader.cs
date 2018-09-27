using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

            var factory = new FileCardParserFactory();
            foreach (string filePath in newFiles)
            {
                var parser = factory.GetParser(filePath);

                var cardSet = parser.Parse();

                _cardSetRepository.Add(cardSet);
            }
        }
    }

    public class FileCardParserFactory
    {
        public IFileCardParser GetParser(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            var content = File.ReadAllLines(filePath);
            if (content.Count() < 1)
                return EmptyCardParser.Empty;

            var firstLine = content.FirstOrDefault();

            if (extension.Equals(".json", StringComparison.InvariantCultureIgnoreCase))
            {
                return new JsonFileCardParser(filePath, content);
            }

            if (firstLine.StartsWith("#"))
            {
                return new HashCommentFileCardParser(filePath, content);
            }

            return EmptyCardParser.Empty;
        }
    }

    public interface IFileCardParser
    {
        CardSetModel Parse();
    }

    public class EmptyCardParser : IFileCardParser
    {
        public static EmptyCardParser Empty = new EmptyCardParser();
        public CardSetModel Parse()
        {
            return CardSetModel.Empty;
        }
    }

    class HashCommentFileCardParser : IFileCardParser
    {
        private readonly string _filePath;
        private readonly IEnumerable<string> _content;
        private static readonly Regex questionPattern = new Regex("^(?<answer>.+)#(?<question>.+)$");
        private static readonly Regex tagLinePattern = new Regex(@"^\s*##\s*(?<tag>.+)$");
        private static readonly Regex subjectPattern = new Regex(@"^#\s+(?<title>.+)$");

        public HashCommentFileCardParser(string filePath, IEnumerable<string> content)
        {
            _filePath = filePath;
            _content = content;
        }

        public CardSetModel Parse()
        {
            var cardSet = new CardSetModel();

            var currentTag = string.Empty;

            foreach (var fileLine in _content)
            {
                try
                {
                    var titleMatch = subjectPattern.Match(fileLine);
                    if (titleMatch.Success && cardSet.Subject == null)
                    {
                        cardSet.Subject = titleMatch.Groups["title"].Value;
                    }

                    var questionAnswer = questionPattern.Match(fileLine);
                    if (questionAnswer.Success)
                    {
                        cardSet.Questions.Add(new CardModel()
                        {
                            Answer = questionAnswer.Groups["answer"].Value,
                            Question = questionAnswer.Groups["question"].Value,
                            Tags = currentTag
                        });
                    }

                    var tag = tagLinePattern.Match(fileLine);
                    if (tag.Success)
                    {
                        currentTag = tag.Groups["tag"].Value;
                    }
                }
                catch (Exception e)
                {
                    Trace.TraceError("Unable to parse line : " + fileLine);
                }
            }

            cardSet.FilePath = _filePath;
            cardSet.Init();
            return cardSet;
        }
    }

    class JsonFileCardParser : IFileCardParser
    {
        private readonly string _filePath;
        private readonly IEnumerable<string> _content;

        public JsonFileCardParser(string filePath, IEnumerable<string> content)
        {
            _filePath = filePath;
            _content = content;
        }

        public CardSetModel Parse()
        {
            try
            {
                var cardSetFileText = string.Join("", _content);
                var cardSet = JsonConvert.DeserializeObject<CardSetModel>(cardSetFileText);
                cardSet.FilePath = _filePath;
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