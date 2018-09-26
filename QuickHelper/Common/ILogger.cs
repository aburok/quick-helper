using System.Collections.ObjectModel;
using QuickHelper.Files;

namespace QuickHelper.Common
{
    public interface ILogger
    {
        ObservableCollection<LogEntry> Logs { get; }

        void Info(string msg);
        void Warning(string msg);
        void Error(string msg);
    }
}