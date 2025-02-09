using GageAndDot.Configuration;

class Program
{
    static void Main(string[] args)
    {
        var server = new Server(AppConfig.Port);
        server.Start();
    }
}