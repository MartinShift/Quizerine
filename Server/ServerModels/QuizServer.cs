using ModelLibrary.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommonLibrary.LibraryModels;

namespace Server.ServerModels
{
    public class QuizServer : IServer
    {
        private ServerCore _core;
        
        public Socket Socket { get; set; }
        public IPAddress Ip { get; set; }
        public IPEndPoint Ep { get; set; }

        public void Worker(Socket s) {
            try
            {
                var buffer = new byte[10000000];
                var read = s.Receive(buffer);
                string raw = Encoding.UTF8.GetString(buffer, 0, read);
                Console.WriteLine(raw);
                DataMessage message = JsonSerializer.Deserialize<DataMessage>(raw);
                var response = "";
                switch (message.Type)
                {
                    case DataType.QuizResult:
                        //Добавити результат у базу даних
                        var result = JsonSerializer.Deserialize<QuizResult>(message.Data);
                        response =  _core.AddQuizResult(result);
                        break;
                    case DataType.AddNewQuiz:
                        var quiz = JsonSerializer.Deserialize<Quiz>(message.Data);
                        response = _core.AddNewQuiz(quiz);
                        break;
                    case DataType.AllQuizzesRequest:
                        response = _core.GetAllQuizzes(); ;
                        break;
                    case DataType.AllQuizResultsRequest:
                        response = _core.GetAllQuizResults();
                        break;
                    case DataType.UpdateQuiz:
                        //Взяти всі(або деякі) Вікторини з бази даних
                        var update = JsonSerializer.Deserialize<Quiz>(message.Data);
                        response = _core.UpdateQuiz(update);
                        break;
                }
                var mes = Encoding.UTF8.GetBytes(response);
                Console.WriteLine(response);
                s.Send(mes);
                //s.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public QuizServer(ServerCore core)
        {
            _core = core;
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            Ip = IPAddress.Parse("127.0.0.1");
            Ep = new IPEndPoint(Ip, 5000);
            Socket.Bind(Ep);
        }

        public void Run()
        {
            while (true)
            {

                Socket.Listen(1000);
                Socket ns = Socket.Accept();
                Console.WriteLine("New socket connected");
                Task.Run(() =>
                {
                    Worker(ns);
                });
            }
        }
    }
}
