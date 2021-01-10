using JarochosDev.WindowsActivityAgent.Common.Models;
using System;

namespace JarochosDev.WindowsActivityAgent.Common
{
    public class WindowsEvent
    {
        public DateTime DateTime { get;set;}
        public string Type { get;set;}
        public string Reason { get;set;}
        public User User {get;set;}
        public Machine Machine { get;set;}
    }
}