using ModelLibrary.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Models
{
    public static class ServerHelper
    {
        public static DataMessage SendToServer(DataMessage message)
        {
            var Ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socket.Connect(Ep);
            var str = JsonSerializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(str);
            socket.Send(bytes);
            var response = new byte[10000000];
            var read = socket.Receive(response);
            var responsestr = Encoding.UTF8.GetString(response, 0, read);
            socket.Close();
            return JsonSerializer.Deserialize<DataMessage>(responsestr);
        }
    }
}
