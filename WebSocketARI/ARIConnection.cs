using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using V8.AddIn;
using AsterNET.ARI.Models;

namespace WebSocketARI
{
    public class AriConnection
    {
        [Alias("ИмяПользователя")]
        public string Name { get; set; }
        [Alias("Пароль")]
        public string Password { get; set; }
        [Alias("IPАдрес")]
        public string Host { get; set; }
        [Alias("Порт")]
        public int Port { get; set; }

        [Alias("Подключить")]
        public bool Connection()
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(Host), Port);

            try
            {
                clientSocket.Connect(serverEndPoint);
                clientSocket.Send(Encoding.ASCII.GetBytes("Action: Login\r\nUsername: " + Name + "\r\nSecret: " + Password + "\r\nActionID: 1\r\nEvents: off\r\nEventmask: call\r\n\r\n"));

                int bytesRead = 0;
                byte[] buffer = new byte[1024];
                bytesRead = clientSocket.Receive(buffer);
                string response = "";
                response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                if (Regex.Match(response, "Message: Authentication accepted", RegexOptions.IgnoreCase).Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}