using System;
using System.IO;

namespace JarochosDev.WindowsActivityAgent.Common.Loggers
{
    public class FileLogger : ICustomLogger
    {
        public string DirectoryPath { get; }

        public FileLogger(string directory)
        {
            DirectoryPath = directory;
        }

        public void Log(string message)
        {        
            if(!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            var fileName = "log_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            var fullFileName = Path.Combine(DirectoryPath, fileName);

            if(!File.Exists(fullFileName))
            {
                using (StreamWriter sw = File.CreateText(fullFileName))
                {
                    sw.WriteLine(DateTime.Now + "     " + message);
                }
            }
            else
            {
                using(StreamWriter sw = File.AppendText(fullFileName))
                {
                    sw.WriteLine(DateTime.Now + "     " + message);
                }
            }
        }
    }
}
