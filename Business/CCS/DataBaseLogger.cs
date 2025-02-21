namespace Business.CCS;



public class DataBaseLogger : ILoger
{
    public void Log()
    {
        System.Console.WriteLine("Logged to DB");
    }
}
