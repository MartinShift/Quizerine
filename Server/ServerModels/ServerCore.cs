using CommonLibrary.LibraryModels;
using ModelLibrary.JsonModels;
using Server.DbModels;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.ServerModels
{
    public static class ServerCore
    {
        public static string AddQuizResult(QuizResult result)
        {
            //  var context = new QuizerineDbContext();
            var dbresult = new QuizResult()
            {
                SecondsSpent = result.SecondsSpent,
                ClientName = result.ClientName,
                Points = result.Points,
                //Знайти в ДБ вікторину з таким самим Id
                //Quiz = 
            };
            //TODO додати результат в базу даних


            //
            var message = new DataMessage()
            {
                Data = "",
                //      Type = DataType.QuizResult
            };
            return JsonSerializer.Serialize(message);
        }
        public static string AddNewQuiz(Quiz quiz)
        {
            var context = new QuizerineDbContext();
            var service = new QuizService(context);
            service.Add(quiz);
            var message = new DataMessage()
            {
                Data = "",
                //    Type = DataType.AddNewQuiz
            };
            return JsonSerializer.Serialize(message);
        }
        public static string GetAllQuizzes()
        {
            // var context = new QuizerineDbContext();
            //TODO взяти всі вікторини

            //
            var message = new DataMessage()
            {
                //TODO серіалізувати всі вікторини в Data
                //Data = ,
                // Type = DataType.AllQuizzesRequest
            };
            return JsonSerializer.Serialize(message);
        }
    }
}
