using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using JarochosDev.WindowsActivityAgent.Common.Loggers;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using JarochosDev.WindowsActivityAgent.Common.Models;

namespace JarochosDev.WindowsActivityAgent.Common
{
    public class WindowsSystemEventListener
    {        
        public string UserName { get; private set; }
        public string PcName { get; private set; }
        public string IP { get; private set; }
        public string Email { get; private set; }
        public bool IsInitialized { get; private set; }
        public EventHandler<WindowsEvent> NewWindowsEventHandler;        

        public void Start()
        {
            if (IsInitialized == false)
            {
                SystemEvents.PowerModeChanged += SystemEventsOnPowerModeChanged;
                SystemEvents.SessionSwitch += SystemEventsOnSessionSwitch;
                SystemEvents.SessionEnded += SystemEventsOnSessionEnded;
                InitializeProperties();
                IsInitialized = true;
            }
        }

        private void InitializeProperties()
        {            
            UserName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').Last();            
            PcName = Dns.GetHostName();            
            IP = Dns.GetHostAddresses(Dns.GetHostName())
                 .Where(IPA => IPA.AddressFamily == AddressFamily.InterNetwork)
                 .Select(x => x.ToString()).FirstOrDefault();
            Email = "";
        }

        private void SystemEventsOnSessionEnded( object sender, SessionEndedEventArgs e)
        {                       
            NewWindowsEventHandler.Invoke(this, BuildwindowsEvent("SessionEndedEvent", e.Reason.ToString()));
        }
        
        private void SystemEventsOnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {                      
            NewWindowsEventHandler.Invoke(this, BuildwindowsEvent("SessionSwitchEvent", e.Reason.ToString());
        }

        private void SystemEventsOnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {                       
            NewWindowsEventHandler.Invoke(this, BuildwindowsEvent("PowerModeChangedEvent", e.Mode.ToString()));
        } 
        
        public void Stop()
        {
            if (IsInitialized == true)
            {
                SystemEvents.PowerModeChanged -= SystemEventsOnPowerModeChanged;
                SystemEvents.SessionSwitch -= SystemEventsOnSessionSwitch;
                SystemEvents.SessionEnded -= SystemEventsOnSessionEnded;
                IsInitialized = false;
            }
        }

        private WindowsEvent BuildwindowsEvent(string eventType, string eventReason)
        {           
            var windowsEvent = new WindowsEvent()
            {
                DateTime = DateTime.UtcNow,
                Machine = new Machine() { IP = IP, Name = PcName },
                Reason = eventReason,
                Type = eventType,
                User = new User(){Name = UserName, Email = Email },
            };
           
            return windowsEvent;
        }
    }
}