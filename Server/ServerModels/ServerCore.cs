using CommonLibrary.LibraryModels;
using ModelLibrary.JsonModels;
using Server.DbModels;
using Server.Services;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server.ServerModels;

public class ServerCore
{
    private QuizerineDbContext _context;
    private IQuizService _quizService;

    public ServerCore(QuizerineDbContext context, IQuizService quizService)
    {
        _context = context;
        _quizService = quizService; 
    }

    public string AddQuizResult(QuizResult result)
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
 
    public DataMessage GetAllQuizzes()
    {
        var quizzes = _quizService.GetAll();
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
        return  message;
    }
    public string GetAllQuizResults()
    {

        var quizzes = GetAllQuizzes();
        
        var results = new List<QuizResult>(); 
        //Коли буде готовий QuizResultService зробити заповнення results

        //
        return  JsonSerializer.Serialize(results.ToList());
    }
    public string UpdateQuiz(Quiz quiz)
    {
        var dbquiz = _context.DbQuizzes.First(x=> x.Id == quiz.Id);
        
        var message = new DataMessage()
        {
            //TODO серіалізувати всі вікторини в Data
            //Data = ,
            // Type = DataType.AllQuizzesRequest
        };
        return JsonSerializer.Serialize(message);
    }

    public string AddNewQuiz(Quiz quiz)
    {
        _quizService.Load(quiz);
        //_quizService.Add(quiz);
        //quiz.Questions.ForEach(x =>
        //{
        //    _quizService.Add(quiz, x); //TODO check
        //    var id = _context.DbQuestions.Last().Id;
        //    x.Answers.ForEach(a => _quizService.Add(id, a)); //TODO check
        //});

        var message = new DataMessage()
        {
            Data = "",
            Type = DataType.AddNewQuiz
        };
        return JsonSerializer.Serialize(message);
    }
}
