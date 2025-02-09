using System.Net.Sockets;
using System.Text;

class ClientHandler
{
    private readonly Socket clientSocket;
    private readonly Server server;
    public string clientName = "Unknown";
    private bool connected;

    public ClientHandler(Socket socket, Server server)
    {
        clientSocket = socket;
        this.server = server;
        connected = true;
    }

    public void HandleClient()
    {
        try
        {
            var buffer = new byte[1024];
            int receivedBytes = clientSocket.Receive(buffer);
            clientName = Encoding.UTF8.GetString(buffer, 0, receivedBytes).Trim();
            Console.WriteLine($"{clientName} подключился к чату.");
            server.BroadcastMessage($"{clientName} вошел в чат", this);

            while (connected)
            {
                try
                {
                    receivedBytes = clientSocket.Receive(buffer);
                    if (receivedBytes == 0) throw new SocketException();

                    var message = Encoding.UTF8.GetString(buffer, 0, receivedBytes).Trim();
                    if (!string.IsNullOrEmpty(message))
                    {
                        Console.WriteLine($"{clientName}: {message}");
                        server.BroadcastMessage($"{clientName}: {message}", this);
                    }
                }
                catch
                {
                    Console.WriteLine($"{clientName} отключился.");
                    server.BroadcastMessage($"{clientName} покинул чат", this);
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка клиента: {ex.Message}");
        }
        finally
        {
            Disconnect();
        }
    }

    public void Disconnect()
    {
        connected = false;
        clientSocket.Close();
        server.RemoveClient(this);
    }

    public void SendMessage(string message)
    {
        try
        {
            var data = Encoding.UTF8.GetBytes(message + "\n");
            clientSocket.Send(data);
        }
        catch
        {
            Disconnect();
        }
    }
}
