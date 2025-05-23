﻿using System;
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

        public static FileLogger Instance()
        {
            return _instance;
        }
        public void Log(params List<string> message)
        {
            var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ";
            foreach (var msg in message)
            {
                logEntry += msg + " ";
            }
            File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
        }
    }
}
