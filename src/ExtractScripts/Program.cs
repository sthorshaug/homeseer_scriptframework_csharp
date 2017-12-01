using System;

namespace ExtractScripts
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            ScriptGrabber grabber = new ScriptGrabber();
            grabber.Run(args);
        }
    }
}
