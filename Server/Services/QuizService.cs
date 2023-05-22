using CommonLibrary.LibraryModels;
using Microsoft.EntityFrameworkCore;
using Server.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class QuizService : IQuizService
    {
        public QuizerineDbContext dbContext = new();
        public bool Add(Quiz quiz)
        {
            var dbquiz = new DbQuiz()
            {
                Image = quiz.Image,
                TimeLimit = quiz.TimeLimit,
                Title = quiz.Title,
            };
            quiz.Questions.ForEach(x =>
            {
                dbquiz.Questions.Add(new DbQuestion()
                {
                    Image = x.Image,
                    Text = x.Text,
                });
                x.Answers.ForEach(a =>
                {
                    dbquiz.Questions.Last().Answers.Add(new DbAnswer()
                    {
                        IsCorrect = a.IsCorrect,
                        Text = a.Text
                    });
                });
            });
            dbContext.DbQuizzes.Add(dbquiz);
            dbContext.SaveChanges();
            return true;
        }

        public bool Add(Quiz quiz, Question question)
        {
            throw new NotImplementedException();
        }

        public bool Add(Question question, Answer answer)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Quiz quiz)
        {
            dbContext.DbQuizzes.Remove(dbContext.DbQuizzes.First(x => x.Id == quiz.Id));
            dbContext.SaveChanges();
            return true;
        }

        public bool Delete(Question question)
        {
            dbContext.DbQuestions.Remove(dbContext.DbQuestions.First(x => x.Id == question.Id));
            dbContext.SaveChanges();
            return true;
        }

        public bool Delete(Answer answer)
        {
            throw new NotImplementedException();
        }

        public ICollection<Quiz> GetAll()
        {
            return dbContext.DbQuizzes
                .Include(x => x.Results)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Answers)
                .Select(x => new Quiz
                {
                    Id = x.Id,
                    Image = x.Image,
                    TimeLimit = x.TimeLimit,
                    Title = x.Title,
                    Questions = x.Questions.Select(q => new Question()
                    {
                        Id = q.Id,
                        Image = q.Image,
                        Text = q.Text,
                        Answers = q.Answers.Select(x => new Answer()
                        {
                            Id = x.Id,
                            IsCorrect = x.IsCorrect,
                            IsSelected = false,
                            Text = x.Text
                        }).ToList()
                    }
                    ).ToList()
                }).ToList(); 
        }

    public bool Update(Quiz quiz)
    {
        throw new NotImplementedException();
    }

    public bool Update(Question question)
    {
        throw new NotImplementedException();
    }

    public bool Update(Answer answer)
    {
        throw new NotImplementedException();
    }
}
}
