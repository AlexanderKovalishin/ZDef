using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using ZDef.Utils;

namespace ZDef.Bootsrtap
{
    public class BootstrapLogView : MonoBehaviour
    {
        [SerializeField] private int _linesLimit = 44;
        [SerializeField] private TMP_Text _logText;
        
        private readonly Queue<string> _logStrings = new();
        private readonly StringBuilder _stringBuilder = new();
        private readonly Stack<string> _stringsStack = new();
        
        private LogListener _logListener;
        private object _logLoc = new();
        private Action _log;

        private void Awake()
        {
            _logListener = new LogListener();
            _logListener.Log += LogListenerOnLog;
        }

        private void OnDestroy()
        {
            _logListener.Log -= LogListenerOnLog;
            _logListener.Dispose();
        }
        
        private void LogListenerOnLog(string message, string stacktrace, LogType type)
        {
            lock (_logStrings)
            {
                _logStrings.Enqueue($"{type}: {message}");
            }

            lock (_logLoc)
            {
                _log = OnLog;
            }
        }

        private void OnLog()
        {
            lock (_logStrings)
            {
                if (_logStrings.Count >= _linesLimit)
                {
                    _logStrings.Dequeue();
                }

                foreach (var logString in _logStrings)
                {
                    _stringsStack.Push(logString);
                }

                while (_stringsStack.Count > 0)
                {
                    _stringBuilder.AppendLine(_stringsStack.Pop());
                }
                
                _logText.SetText(_stringBuilder.ToString());
                _stringBuilder.Clear();
            }
        }

        private void Update()
        {
            lock (_logLoc)
            {
                _log?.Invoke();
                _log = null;
            }
        }
    }
}