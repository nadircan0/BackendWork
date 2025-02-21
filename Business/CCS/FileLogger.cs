using System;

namespace Business.CCS;

public class FileLogger : ILoger
{
    public void Log()
    {
        System.Console.WriteLine("Logged to file");
    }
}
