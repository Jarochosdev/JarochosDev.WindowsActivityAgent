using System;
using System.Collections.Generic;
using System.Text;

namespace JarochosDev.WindowsActivityAgent.Common.Loggers
{
    public class DisplayLogger : ICustomLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}