using System.Text.Json;

namespace GageAndDot.Configuration;

public class AppConfig
{
    
    public static string idAddress { get; set; } = "127.0.0.1";
    public static int Port { get; set; } = 8888;
    
    private AppConfig()
    {
        var config = ReadJsonFile();
    }

    public async Task ReadJsonFile(AppConfig config = null)
    {
        if (File.Exists("config.json"))
        {
            var fileConfig = await File.ReadAllTextAsync("config.json");
            config = JsonSerializer.Deserialize<AppConfig>(fileConfig);
        }
        else
        {
            Console.WriteLine("файл конфигурации сервера 'config.json' не найден");
            config = new AppConfig();
        }
    }
}