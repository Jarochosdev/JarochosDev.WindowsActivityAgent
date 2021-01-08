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
            WindowsSystemEventListener.Start();
        }

        public void Stop()
        {
            WindowsSystemEventListener.Stop();
        }
    }
}