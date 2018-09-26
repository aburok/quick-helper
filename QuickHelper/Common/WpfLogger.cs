using System.Collections.ObjectModel;

namespace QuickHelper.Common
{
    public class WpfLogger : ILogger
    {
        public ObservableCollection<LogEntry> Logs { get; }

        public WpfLogger()
        {
            Logs = new ObservableCollection<LogEntry>();
        }

        public void Info(string msg)
        {
            Logs.Add(LogEntry.Info(msg));
        }

        public void Warning(string msg)
        {
            Logs.Add(LogEntry.Warning(msg));
        }

        public void Error(string msg)
        {
            Logs.Add(LogEntry.Error(msg));
        }
    }
}