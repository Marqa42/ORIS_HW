using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GageAndDot;

class Server
{
    private Socket listenerSocket;
    public List<ClientHandler> clients = new List<ClientHandler>();
    private readonly int port;

    public Server(int port)
    {
        this.port = port;
        listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public void Start()
    {
        try
        {
            listenerSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            listenerSocket.Listen(10);
            Console.WriteLine("Сервер запущен. Ожидание подключений...");

            while (true)
            {
                var clientSocket = listenerSocket.Accept();
                var clientHandler = new ClientHandler(clientSocket, this);
                clients.Add(clientHandler);
                Task.Run(() => clientHandler.HandleClient());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сервера: {ex.Message}");
        }
        finally
        {
            Stop();
        }
    }
    

    public void BroadcastMessage(string message, ClientHandler sender)
    {
        foreach (var client in clients)
        {
            client.SendMessage(message);
            Console.WriteLine("отправил все");
        }
    }

    public void RemoveClient(ClientHandler client)
    {
        clients.Remove(client);
    }

    public void Stop()
    {
        foreach (var client in clients)
        {
            client.Disconnect();
        }
        listenerSocket.Close();
        Console.WriteLine("Сервер остановлен.");
    }
}