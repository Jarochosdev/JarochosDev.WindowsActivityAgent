using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using JarochosDev.WindowsActivityAgent.Common.Loggers;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace JarochosDev.WindowsActivityAgent.Common
{
    public class WindowsSystemEventListener
    {
        public LogManager LogManager { get; }
        public string UserName { get; private set; }
        public string PcName { get; private set; }
        public string IP { get; private set; }

        public WindowsSystemEventListener(LogManager logManager)
        {
            LogManager = logManager;
        }

        public void Start()
        {
            SystemEvents.PowerModeChanged += SystemEventsOnPowerModeChanged;
            SystemEvents.SessionSwitch += SystemEventsOnSessionSwitch;
            SystemEvents.SessionEnded += SystemEventsOnSessionEnded;
            InitializePropierties();
        }
        private void InitializePropierties()
        {            
            UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').Last();            
            PcName = Dns.GetHostName();            
            IP = Dns.GetHostAddresses(Dns.GetHostName())
                 .Where(IPA => IPA.AddressFamily == AddressFamily.InterNetwork)
                 .Select(x => x.ToString()).FirstOrDefault();
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
