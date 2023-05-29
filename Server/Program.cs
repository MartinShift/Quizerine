using CommonLibrary.LibraryModels;
using Microsoft.EntityFrameworkCore;
using Server.DbModels;
using Server.Services;

QuizerineDbContext dbContext = new QuizerineDbContext();
QuizService service = new QuizService(dbContext);
QuizResultService quizResult = new QuizResultService(dbContext);
service.Load(new Quiz() { Title = "New Quiz", TimeLimit = 60});
var res = service.GetAll().Select(x => new Quiz()
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
}).ToList();
Quiz q = res[0];
q.Title = "New title 1";
q.Questions.Add(new Question() { Text = "New Added question" });
service.Load(q);
service.GetAll().ToList().ForEach(q =>
{
    Console.WriteLine("Quiz: " + q.Title);
    q.Questions.ToList().ForEach(qu =>
    {
        Console.WriteLine("Question: " + qu.Text);
        qu.Answers.ToList().ForEach(a =>
        {
            Console.WriteLine("Answer: " + a.Text + " " + a.IsCorrect);
        });
    });
});
quizResult.Load(new QuizResult()
{
    SecondsSpent = 60,
    Points = 100,
    Quiz = q,
    ClientName = "Echo",
});
quizResult.GetAll().ToList().ForEach(x =>
{
    Console.WriteLine(x.Quiz.Title + " " + x.ClientName);
});
Console.WriteLine(quizResult.HasPassed("Echo", q));