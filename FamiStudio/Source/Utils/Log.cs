﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamiStudio
{
    public enum LogSeverity
    {
        Info,
        Warning,
        Error
    };

    public interface ILogOutput
    {
        void LogMessage(string msg);
    }

    public class ListLogOutput : ILogOutput
    {
        private List<string> messages = new List<string>();

        public void LogMessage(string msg)
        {
            messages.Add(msg);
        }

        public bool IsEmpty => messages.Count == 0;
        public List<string> Messages => messages;
    }

    public class ScopedLogOutput : IDisposable
    {
        public ScopedLogOutput(ILogOutput log, LogSeverity minSeverity = LogSeverity.Info)
        {
            Log.LogOutput = log;
            Log.MinSeverity = minSeverity;
        }

        public void Dispose()
        {
            Log.LogOutput = null;
        }
    }

    public static class Log
    {
        private static readonly string[] SeverityStrings = new []
        {
            "Info: ",
            "Warning: ",
            "Error: "
        };

        // HACK: See comment above. Mono compiler bug.
        public static ILogOutput LogOutput;
        public static LogSeverity MinSeverity = LogSeverity.Info;

        public static void LogMessage(LogSeverity severity, string msg)
        {
            if ((int)severity >= (int)MinSeverity && LogOutput != null)
            {
                LogOutput.LogMessage(SeverityStrings[(int)severity] + msg);
                Debug.WriteLine(SeverityStrings[(int)severity] + msg);
            }
        }
    };
}
