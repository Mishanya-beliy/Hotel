using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;

namespace Hotel.WEB.Additional
{
    public class FileLogger : ILogger
    {
        public  static string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "logger.json");
        private readonly string _filePath;
        private static object _lock = new object();
        private int ID = 0;

        public FileLogger(string filePath)
        {
            _filePath = filePath;

            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                if (lines.Length != 0)
                    ID = lines.Length;
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    var log = new Log(ID++, logLevel.ToString(), formatter(state, exception));
                    string jsonString = JsonSerializer.Serialize(log);
                    File.AppendAllText(_filePath, jsonString + Environment.NewLine);
                }
            }
        }

        internal static Log[] ReadLog()
        {
            var lines = File.ReadAllLines(FilePath);
            Log[] logs = new Log[lines.Length];
            for(int i = 0; i < lines.Length; i++)
            {
                logs[i]  = JsonSerializer.Deserialize<Log>(lines[i]);
            }

            return logs;
        }
    }

    public class Log
    {
        public Log()
        {

        }
        public Log(int eventId, string logLevel, string message)
        {
            EventId = eventId;
            LogLevel = logLevel;
            Message = message;
        }

        public int EventId { set; get; }
        public string LogLevel { set; get; }
        public string Message { set; get; }
    }
}
