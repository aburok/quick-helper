using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace QuickHelper.Files
{
    public interface IFileWatcher
    {
        ObservableCollection<WatchedFile> LoadedFiles { get; }

        IEnumerable<string> GetAnkiFilePathList();

        void AddFile(string filePath);

        bool ShouldBeRead(string filePath);
    }

}