using CommonLibrary.LibraryModels;
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
    public class ServerHelper
    {
        private IPEndPoint _serverEndPoint;
        public ServerHelper(string serverIp, int port)
        {
            _serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), port);
        }

        //Results
        public void SendQuizResult(QuizResult message)
        {
            var datamessage = new DataMessage()
            {
                Data = JsonSerializer.Serialize(message),
                Type = DataType.QuizResult
            };
            sendToServer(datamessage);
        }
        public List<QuizResult> GetQuizResults()
        {
            var datamessage = new DataMessage()
            {
                Data = "",
                Type = DataType.AllQuizResultsRequest
            };
            var response = sendToServer(datamessage);
            var data = JsonSerializer.Deserialize<List<QuizResult>>(response.Data);
            return data;
        }

        //Quizzes
        public void SendNewQuiz(Quiz message)
        {
            var datamessage = new DataMessage()
            {
                Data = JsonSerializer.Serialize(message),
                Type = DataType.AddNewQuiz
            };
            sendToServer(datamessage);
        }
        public List<Quiz> GetQuizzes()
        {
            var datamessage = new DataMessage()
            {
                Data = "",
                Type = DataType.AllQuizzesRequest
            };
            var response = sendToServer(datamessage);
            var data = JsonSerializer.Deserialize<List<Quiz>>(response.Data);
            return data;
        }
        public void UpdateQuiz(Quiz quiz)
        {
            var datamessage = new DataMessage()
            {
                Data = JsonSerializer.Serialize(quiz),
                Type = DataType.UpdateQuiz
            };
            sendToServer(datamessage);
        }
        private DataMessage sendToServer(DataMessage message)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            socket.Connect(_serverEndPoint);
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
