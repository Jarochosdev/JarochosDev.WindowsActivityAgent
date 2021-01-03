using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using JarochosDev.WindowsActivityAgent.Common.Loggers;

namespace JarochosDev.WindowsActivityAgent.Common
{
    public class WindowsSystemEventListener
    {
        public LogManager LogManager { get; }

        public WindowsSystemEventListener(LogManager logManager)
        {
            LogManager = logManager;
        }

        public void Start()
        {
            SystemEvents.PowerModeChanged += SystemEventsOnPowerModeChanged;
            SystemEvents.SessionSwitch += SystemEventsOnSessionSwitch;
            SystemEvents.SessionEnded += SystemEventsOnSessionEnded;
        }

        private void SystemEventsOnSessionEnded(object sender, SessionEndedEventArgs e)
        {             
            LogManager.Log($"SessionEndedEvent:{e.Reason}");           
        }

        private void SystemEventsOnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            LogManager.Log($"SessionSwitchEvent:{e.Reason}");
        }

        private void SystemEventsOnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {            
            LogManager.Log($"PowerModeChangedEvent:{e.Mode}");            
        }        

        public void Stop()
        {
            SystemEvents.PowerModeChanged -= SystemEventsOnPowerModeChanged;
            SystemEvents.SessionSwitch -= SystemEventsOnSessionSwitch;
            SystemEvents.SessionEnded -= SystemEventsOnSessionEnded;
        }
    }
}
