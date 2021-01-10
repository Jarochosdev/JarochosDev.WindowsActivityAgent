using System;
using System.Collections.Generic;
using System.Text;

namespace JarochosDev.WindowsActivityAgent.Common
{
    public class WindowsSystemEventService
    {
        public WindowsSystemEventListener WindowsSystemEventListener { get; }
        
        public WindowsSystemEventService(WindowsSystemEventListener windowsSystemEventListener)
        {
            WindowsSystemEventListener = windowsSystemEventListener;
        }

        public void Start()
        {
            WindowsSystemEventListener.NewWindowsEventHandler += OnNewEventHandler;
            WindowsSystemEventListener.Start();
        }

        private void OnNewEventHandler(object sender, WindowsEvent e)
        {
            Console.WriteLine("New Event: " + e.Type + " " + e.Reason + " " + e.User.Name);
            //Send to save in a database
        }

        public void Stop()
        {
            WindowsSystemEventListener.NewWindowsEventHandler -= OnNewEventHandler;
            WindowsSystemEventListener.Stop();
        }
    }
}