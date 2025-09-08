using System;
using UnityEngine;

namespace ZDef.Utils
{
    public class LogListener: IDisposable
    {
        public delegate void LoggerDelegate(string message, string stackTrace, LogType type);

        public event LoggerDelegate Log;

        public LogListener()
        {
            //Application.logMessageReceived += ApplicationOnLogMessageReceived;
            Application.logMessageReceivedThreaded += ApplicationOnLogMessageReceived;
        }

        public void Dispose()
        {
            //Application.logMessageReceived -= ApplicationOnLogMessageReceived;
            Application.logMessageReceivedThreaded -= ApplicationOnLogMessageReceived;
        }

        private void ApplicationOnLogMessageReceived(string condition, string stacktrace, LogType type)
        {
            Log?.Invoke(condition, stacktrace, type);
        }
    }
}