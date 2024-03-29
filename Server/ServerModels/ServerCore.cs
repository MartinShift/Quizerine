﻿using CommonLibrary.LibraryModels;
using Microsoft.EntityFrameworkCore;
using ModelLibrary.JsonModels;
using Server.DbModels;
using Server.Services;
using Server.Services.Interfaces;
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
    private IQuizResultService _quizResultService;

    public ServerCore(QuizerineDbContext context, IQuizService quizService, IQuizResultService quizResultService)
    {
        _context = context;
        _quizService = quizService;
        _quizResultService = quizResultService;
    }

    public string AddQuizResult(QuizResult result)
    {
        var quiz = _context.DbQuizzes.First(x => x.Id == result.Quiz.Id);
        
        var dbresult = new DbQuizResult()
        {
            SecondsSpent = result.SecondsSpent,
            ClientName = result.ClientName,
            Points = result.Points,
            Quiz = quiz,
        };
        _context.DbResults.Add(dbresult);
        _context.SaveChanges();

        //
        var message = new DataMessage()
        {
            Data = "",
            Type = DataType.QuizResult
        };
        return JsonSerializer.Serialize(message);
    }

    public string GetAllQuizzes()
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
        });
        var message = new DataMessage()
        {
            Data = JsonSerializer.Serialize(res.ToList()),
            Type = DataType.AllQuizzesRequest
        };
        return JsonSerializer.Serialize(message);

    }
    public string GetAllQuizResults()
    {
        var message = new DataMessage
        {
            Data = JsonSerializer.Serialize(
                _context
                .DbResults
                .Include(x => x.Quiz)
                .Select(dbResult => new QuizResult
                {
                    ClientName = dbResult.ClientName,
                    Points = dbResult.Points,
                    SecondsSpent = dbResult.SecondsSpent,
                    Quiz = new Quiz
                    {
                        Id = dbResult.Quiz.Id,
                        Title = dbResult.Quiz.Title,
                        Image = dbResult.Quiz.Image,
                        Questions = new(),
                    }
                })),
            Type = DataType.AllQuizResultsRequest
        };
        return JsonSerializer.Serialize(message);
    }
    public string UpdateQuiz(Quiz quiz)
    {
        var dbquiz = _context.DbQuizzes.First(x => x.Id == quiz.Id);

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

        var message = new DataMessage()
        {
            Data = "",
            Type = DataType.AddNewQuiz
        };
        return JsonSerializer.Serialize(message);
    }
}
