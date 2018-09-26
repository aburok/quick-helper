using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using QuickHelper.Common;
using QuickHelper.Configuration;

namespace QuickHelper.Files
{
    public class FileWatcher : IFileWatcher
    {
        private readonly IAppConfig _appConfig;
        private readonly ILogger _logger;

        public FileWatcher(IAppConfig _appConfig, ILogger logger)
        {
            this._appConfig = _appConfig;
            _logger = logger;
        }

        public ObservableCollection<WatchedFile> LoadedFiles { get; private set; }
            = new ObservableCollection<WatchedFile>();

        public IEnumerable<string> GetAnkiFilePathList()
        {
            _logger.Info("Reading files from list : " + _appConfig.SemicolonSeparatedFilePaths);
            if (string.IsNullOrWhiteSpace(_appConfig.SemicolonSeparatedFilePaths))
                return Enumerable.Empty<string>();

            var files = GetAnkiFilesFromConfiguration();

            var newFiles = CheckForNewFiles(files);
            return newFiles;
        }

        private IEnumerable<string> GetAnkiFilesFromConfiguration()
        {
            var directories = _appConfig.SemicolonSeparatedFilePaths.Split(';');

            var existingDirectories = directories.Where(Directory.Exists);

            var files =
                existingDirectories.SelectMany(dir => Directory.EnumerateFiles(dir, "*.anki.json", SearchOption.AllDirectories));
            return files;
        }

        private IEnumerable<string> CheckForNewFiles(IEnumerable<string> files)
        {
            var newFiles = new List<string>();

            foreach (var file in files)
            {
                var lastWrite = File.GetLastWriteTime(file);
                var alreadyWatchedFile = LoadedFiles.FirstOrDefault(f => f.FilePath == file);
                if (alreadyWatchedFile != null)
                {
                    var savedLastWrite = alreadyWatchedFile.LastWrite;
                    if (lastWrite > savedLastWrite)
                    {
                        alreadyWatchedFile.LastWrite = lastWrite;
                        newFiles.Add(file);
                    }
                }
                else
                {
                    LoadedFiles.Add(new WatchedFile(file, lastWrite));
                    newFiles.Add(file);
                }
            }

            return newFiles;
        }

        public void AddFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public bool ShouldBeRead(string filePath)
        {
            throw new NotImplementedException();
        }
    }

    public class WatchedFile
    {
        public WatchedFile(string filePath, DateTime lastWrite)
        {
            FilePath = filePath;
            LastWrite = lastWrite;
        }

        public string FilePath { get; set; }
        public DateTime LastWrite { get; set; }
    }
}