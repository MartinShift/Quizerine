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
            var context = new QuizerineDbContext();
            var dbresult = new DbQuizResult()
            {
                SecondsSpent = result.SecondsSpent,
                ClientName = result.ClientName,
                Points = result.Points,
                Quiz = context.DbQuizzes.First(x=> x.Id == result.Quiz.Id)
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
        public static string AddNewQuiz(Quiz quiz)
        {
            var context = new QuizerineDbContext();
            var service = new QuizService(context);
            service.Add(quiz);
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
            var service = new QuizService(context);
            var quizzes = service.GetAll();
            var res = quizzes.Select(x => new Quiz()
            {
                Id = x.Id,
                Image = x.Image,
                TimeLimit = x.TimeLimit,
                Title = x.Title,
                Questions = x.Questions.Select(q => new Question
                {
                    Id = q.Id,
                    Image = q.Image,
                    Text = q.Text,
                    Answers = q.Answers.Select(a => new Answer 
                    {
                        Id = a.Id,
                        IsCorrect = a.IsCorrect,
                        Text = a.Text,
                    }).ToList(),
                    
                }).ToList()
            }) ;
            var message = new DataMessage()
            {
                Data = JsonSerializer.Serialize(res.ToList()),
                Type = DataType.AllQuizzesRequest
            }; 
            return  JsonSerializer.Serialize(message);
        }
        public static string GetAllQuizResults()
        {

            var quizzes = GetAllQuizzes();
            var context = new QuizerineDbContext();
            
            var results = new List<QuizResult>(); 
            //Коли буде готовий QuizResultService зробити заповнення results

            //
            return  JsonSerializer.Serialize(results.ToList());
        }
        public static string UpdateQuiz(Quiz quiz)
        {
            var context = new QuizerineDbContext();
            var dbquiz = context.DbQuizzes.First(x=> x.Id == quiz.Id);
            
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
