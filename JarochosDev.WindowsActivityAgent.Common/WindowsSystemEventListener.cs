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
        public string UserName { get; set; }
        public string PcName { get; set; }
        public string IP { get; set; }

        public WindowsSystemEventListener(LogManager logManager)
        {
            LogManager = logManager;
        }

        public void Start()
        {
            SystemEvents.PowerModeChanged += SystemEventsOnPowerModeChanged;
            SystemEvents.SessionSwitch += SystemEventsOnSessionSwitch;
            SystemEvents.SessionEnded += SystemEventsOnSessionEnded;
            PcPropierties();
        }
        private void PcPropierties()
        {
            UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').Last();
            PcName = Dns.GetHostName();
            IP = Dns.GetHostByName(PcName).AddressList[0].ToString(); ;
        }

        private void SystemEventsOnSessionEnded( object sender, SessionEndedEventArgs e)
        {
            var message = $"SessionEndedEvent:{e.Reason},{UserName},{PcName},{IP}";
            LogManager.Log(message);           
        }

        private void SystemEventsOnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            var message = $"SessionSwitchEvent:{e.Reason},{UserName},{PcName},{IP}";
            LogManager.Log(message);                       
        }

        private void SystemEventsOnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            var message = $"PowerModeChangedEvent:{e.Mode},{UserName},{PcName},{IP}";
            LogManager.Log(message);                                   
        }        

        public void Stop()
        {
            SystemEvents.PowerModeChanged -= SystemEventsOnPowerModeChanged;
            SystemEvents.SessionSwitch -= SystemEventsOnSessionSwitch;
            SystemEvents.SessionEnded -= SystemEventsOnSessionEnded;
        }
    }
}
