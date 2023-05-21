using CommonLibrary.LibraryModels;
using ModelLibrary.JsonModels;
using Server.DbModels;
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
        public static string AddQuizResult(string resultData)
        {
            var result = JsonSerializer.Deserialize<QuizResult>(resultData);
            var context = new QuizerineDbContext();
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
                Type = DataType.QuizResult
            };
            return JsonSerializer.Serialize(message);
        }
        public static string AddNewQuiz(string quizData)
        {
            var result = JsonSerializer.Deserialize<Quiz>(quizData);
            var context = new QuizerineDbContext();
            var Quiz = new DbQuiz
            {
                Image = result.Image,
                Title = result.Title,
                TimeLimit = result.TimeLimit,
            };
            result.Questions.ForEach(x=>
            {
                Quiz.Questions.Add(x);
                x.Answers.ForEach(x =>
                {
                    Quiz.Questions.Last().Answers.Add(x);
                });
            });

            var message = new DataMessage()
            {
                Data = "",
                Type = DataType.AddNewQuiz
            };
            return JsonSerializer.Serialize(message);
        }
        public static string GetAllQuizzes()
        {
            var context = new QuizerineDbContext();
            //TODO взяти всі вікторини
            
            //
            var message = new DataMessage()
            {
                //TODO серіалізувати всі вікторини в Data
                //Data = ,
                Type = DataType.AllQuizzesRequest
            };
            return JsonSerializer.Serialize(message);
        }
    }
}
