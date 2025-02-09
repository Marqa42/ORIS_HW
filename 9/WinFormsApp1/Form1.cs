using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using GageAndDot.Configuration;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private bool isUserNameSet = false;
        private string userName = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            userName = textBox2.Text;
            clientSocket.Connect(AppConfig.idAddress, AppConfig.Port);
            await SendMessagesAsync(clientSocket, userName);
            textBox2.Visible = false;
            button2.Visible = false;
            TextPanel.Visible = true;
            textBox1.Visible = true;
            button1.Visible = true;
            _ = ReceiveMessagesAsync();
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            var inputText = textBox1.Text;

            if (string.IsNullOrWhiteSpace(inputText))
            {
                MessageBox.Show("Поле не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            textBox1.Clear();
            await SendMessagesAsync(clientSocket, inputText);
        }

        private static async Task SendMessagesAsync(Socket clientSocket, string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message + "\n");
            await clientSocket.SendAsync(new ArraySegment<byte>(data), SocketFlags.None);
        }

        private async Task ReceiveMessagesAsync()
        {
            var buffer = new byte[1024];
            while (clientSocket.Connected)
            {
                try
                {
                    // Асинхронно получаем данные от сервера
                    int receivedBytes = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                    if (receivedBytes == 0)
                    {
                        MessageBox.Show("Сервер отключился.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes).Trim();
                    TextPanel.Invoke((MethodInvoker)(() =>
                    { TextPanel.Items.Add(receivedMessage);
                    }));
                }
                catch (SocketException)
                {
                    MessageBox.Show("Соединение с сервером потеряно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при получении сообщения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }
            }
        }
    }
} 