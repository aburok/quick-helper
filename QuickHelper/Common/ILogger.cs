namespace QuickHelper.Common
{
    public interface ILogger
    {
        void Info(string msg);
        void Warning(string msg);
        void Error(string msg);
    }

    public class Logger : ILogger
    {
        public void Info(string msg)
        {
        }

        public void Warning(string msg)
        {
        }

        public void Error(string msg)
        {
        }
    }
}