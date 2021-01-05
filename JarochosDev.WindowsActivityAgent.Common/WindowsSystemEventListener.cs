using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Text;
using JarochosDev.WindowsActivityAgent.Common.Loggers;
using System.Linq;
using System.Net;

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

        private void SystemEventsOnSessionEnded( object sender, SessionEndedEventArgs e)
        {
            var userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').Last();
            var pcName = Dns.GetHostName();
            var ip = Dns.GetHostByName(pcName).AddressList[0].ToString(); ;
            var message = $"SessionEndedEvent:{e.Reason},{userName},{pcName},{ip}";
            LogManager.Log(message);           
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
