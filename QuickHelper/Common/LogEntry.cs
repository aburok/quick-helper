using System;

namespace QuickHelper.Common
{
    public class LogEntry
    {
        public DateTime Time { get; }
        public string Level { get; }
        public string Message { get; }
        private LogEntry(string message, string level, DateTime time)
        {
            Message = message;
            Level = level;
            Time = time;
        }

        public static LogEntry Info(string message)
        {
            return new LogEntry(message, "Info", DateTime.Now );
        }


        public static LogEntry Warning(string message)
        {
            return new LogEntry(message, "Warning", DateTime.Now );
        }
        public static LogEntry Error(string message)
        {
            return new LogEntry(message, "Error", DateTime.Now );
        }
    }
}