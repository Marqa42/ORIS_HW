using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GageAndDot.Configuration;

class Client
{
    static async Task Main(string[] args)
    {
        Console.Write("Введите свое имя: ");   
        string? userName = Console.ReadLine();
        Console.WriteLine($"Добро пожаловать, {userName}");

        using Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            clientSocket.Connect(AppConfig.idAddress, AppConfig.Port);
            Console.WriteLine("Подключено к серверу.");

            // Запуск получения сообщений
            _ = Task.Run(() => ReceiveMessagesAsync(clientSocket));

            // Отправка сообщений
            await SendMessagesAsync(clientSocket, userName);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static async Task SendMessagesAsync(Socket clientSocket, string userName)
    {
        try
        {
            // Сначала отправляем имя пользователя
            byte[] nameData = Encoding.UTF8.GetBytes(userName + "\n");
            clientSocket.Send(nameData);

            Console.WriteLine("Для отправки сообщений введите сообщение и нажмите Enter");

            while (true)
            {
                string? message = Console.ReadLine();
                if (string.IsNullOrEmpty(message)) continue;

                byte[] data = Encoding.UTF8.GetBytes(message + "\n");
                clientSocket.Send(data);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка отправки сообщений: {ex.Message}");
        }
    }

    static async Task ReceiveMessagesAsync(Socket clientSocket)
    {
        try
        {
            byte[] buffer = new byte[1024];

            while (true)
            {
                int receivedBytes = clientSocket.Receive(buffer);
                if (receivedBytes > 0)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                    Print(message);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения сообщений: {ex.Message}");
        }
    }

    static void Print(string message)
    {
        if (OperatingSystem.IsWindows())
        {
            var position = Console.GetCursorPosition();
            int left = position.Left;
            int top = position.Top;
            Console.MoveBufferArea(0, top, left, 1, 0, top + 1);
            Console.SetCursorPosition(0, top);
            Console.WriteLine(message);
            Console.SetCursorPosition(left, top + 1);
        }
        else
        {
            Console.WriteLine(message);
        }
    }
}
