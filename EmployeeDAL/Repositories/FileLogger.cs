using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDAL.Repositories
{
    public sealed partial class FileLogger
    {
        private static readonly FileLogger _instance = new FileLogger();
        private static readonly string logFilePath = @"../EmployeeDAL/log.txt";
        private static readonly Lock _lock = new();    // C#13 feature: Lock type

        // Partial property declaration
        public partial string LogHeader { get; set; }

        public static FileLogger Instance()
        {
            return _instance;
        }

        public void Log(params List<string> messages)  // C#13 feature: params collections
        {
            File.AppendAllText(logFilePath, $"{DateTime.Now}: {LogHeader}");
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

    // Another part of the partial class defining the property implementation
    public sealed partial class FileLogger
    {
        private string _logHeader = "System Log: ";

        public partial string LogHeader
        {
            get => _logHeader;
            set => _logHeader = value;
        }
    }
}
