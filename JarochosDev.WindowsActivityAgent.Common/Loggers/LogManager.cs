using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JarochosDev.WindowsActivityAgent.Common.Loggers
{
    public class LogManager
    {
        public IEnumerable<ICustomLogger> DisplayPrints { get; }
        
        public LogManager(IEnumerable<ICustomLogger> displayPrints)
        {            
            DisplayPrints = displayPrints;
        }

        public void Log(string message)
        {
            foreach(var displayPrint in DisplayPrints)
            {
                displayPrint.Log(message);
            }                                                
        }
    }
}
