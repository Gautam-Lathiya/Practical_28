using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL.Repositories
{
    public sealed class FileLogger
    {
        private static readonly FileLogger _instance = new FileLogger();
        private static readonly string logFilePath = @"../EmployeeDAL/log.txt";
        private static readonly Lock _lock = new();    // C#13 feature: Lock type

        public static FileLogger Instance()
        {
            return _instance;
        }
        public void Log(params List<string> messages)  // C#13 feature: params collections
        {
            File.AppendAllText(logFilePath, $"{DateTime.Now}: ");
            messages.ForEach(LogMessage); // C#13 feature: Method group natural type
        }

        public void LogMessage(string message)
        {
            lock (_lock)
            {
                File.AppendAllText(logFilePath, $"{message}{Environment.NewLine}");
            }
        }
    }
}
