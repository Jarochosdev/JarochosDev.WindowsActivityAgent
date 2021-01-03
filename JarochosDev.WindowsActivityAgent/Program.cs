using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JarochosDev.WindowsActivityAgent.Common;
using JarochosDev.WindowsActivityAgent.Common.Loggers;

namespace JarochosDev.WindowsActivityAgent
{
    class Program
    {
        static void Main(string[] args)
        {                        
            var currentDirectory = Directory.GetCurrentDirectory();
            var logsDirectory = Path.Combine(currentDirectory, "Logs");

            var allDisplayPrints = new List<ICustomLogger>(){new DisplayLogger(), new FileLogger(logsDirectory)};
            var logManager = new LogManager(allDisplayPrints);

            var windowsSystemEventService = new WindowsSystemEventService(new WindowsSystemEventListener(logManager));
            windowsSystemEventService.Start();
            Console.WriteLine("Ivanna Marquez");

            Console.ReadLine();
            windowsSystemEventService.Stop();
        }
    }
}
