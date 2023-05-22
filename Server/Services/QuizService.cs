﻿using CommonLibrary.LibraryModels;
using DbModels.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Server.DbModels;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    internal class QuizService : IQuizService
    {
        Repository<DbQuiz> quizRepository { get; set; }
        Repository<DbQuestion> questionRepository { get; set; }
        Repository<DbQuizResult> quizResultRepository { get; set; }
        Repository<DbAnswer> answerRepository { get; set; }
        QuizerineDbContext _context { get; set; }
        public QuizService(QuizerineDbContext quizerineDb)
        {
            quizRepository = new Repository<DbQuiz>(quizerineDb);
            quizResultRepository = new Repository<DbQuizResult>(quizerineDb);
            answerRepository = new Repository<DbAnswer>(quizerineDb);
            questionRepository = new Repository<DbQuestion>(quizerineDb);
            _context = quizerineDb;
        }
        public bool HasPassed(string client, int quizid) => quizResultRepository.ExistsAsync(qr => qr.ClientName == client && qr.QuizId == quizid).Result;
        public void Add(Quiz quiz)
        {
            var dbquiz = new DbQuiz()
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Image = quiz.Image,
                TimeLimit = quiz.TimeLimit,
            };
            quizRepository.AddAsync(dbquiz).Wait();
        }

        public void Add(Quiz quiz, Question question)
        {
            Add(quiz);
            var dbquestion = new DbQuestion()
            {
                Id = question.Id,
                Text = question.Text,
                Image = question.Image,
                QuizId = quiz.Id,
            };
            questionRepository.AddAsync(dbquestion).Wait();
        }
        public void Add(int quizId, Question question)
        {
            var dbquestion = new DbQuestion()
            {
                Id = question.Id,
                Text = question.Text,
                Image = question.Image,
                QuizId = quizId,
            };
            questionRepository.AddAsync(dbquestion).Wait();
        }

        public void Add(int quizId, Question question, Answer answer)
        {
            var dbquestion = new DbQuestion()
            {
                Id = question.Id,
                Text = question.Text,
                Image = question.Image,
                QuizId = quizId,
            };
            questionRepository.AddAsync(dbquestion).Wait();
            var dbanswer = new DbAnswer()
            {
                Id = answer.Id,
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
            };
            answerRepository.AddAsync(dbanswer).Wait();
        }

        public void Delete(Quiz quiz)
        {
            quizRepository.DeleteAsync(quiz.Id).Wait();
        }

        public void Delete(Question question)
        {
            questionRepository.DeleteAsync(question.Id).Wait();
        }

        public void Delete(Answer answer)
        {
            answerRepository.DeleteAsync(answer.Id).Wait();
        }

        public void Update(Quiz quiz)
        {
            var dbquiz = new DbQuiz()
            {
                Title = quiz.Title,
                Image = quiz.Image,
                TimeLimit = quiz.TimeLimit,
            };
            quizRepository.UpdateAsync(dbquiz).Wait();
        }

        public void Update(Question question)
        {
            var dbquestion = new DbQuestion()
            {
                Text = question.Text,
                Image = question.Image,
            };
            questionRepository.UpdateAsync(dbquestion).Wait();
        }

        public void Update(Answer answer)
        {
            var dbanswer = new DbAnswer()
            {
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
            };
            answerRepository.UpdateAsync(dbanswer).Wait();
        }

        public void Add(int questionId, Answer answer)
        {
            var dbanswer = new DbAnswer()
            {
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
                QuestionId = questionId,
            };
            answerRepository.AddAsync(dbanswer).Wait();
        }

        public ICollection<DbQuiz> FindQuizByCondition(Expression<Func<DbQuiz, bool>> expression)
        {
            return _context.DbQuizzes.Where(expression).Include(x => x.Questions).Include(x => x.Results).ToList();
        }

        public ICollection<DbQuestion> FindQuestionByCondition(Expression<Func<DbQuestion, bool>> expression)
        {
            return _context.DbQuestions.Where(expression).Include(x => x.Answers).ToList();
        }

        public ICollection<DbAnswer> FindAnswerByCondition(Expression<Func<DbAnswer, bool>> expression)
        {
            return answerRepository.FindByConditionAsync(expression).Result;
        }

        public ICollection<DbQuiz> GetAll()
        {
            return _context.DbQuizzes.Include(x => x.Results).Include(x => x.Questions).ToList();
        }

        public ICollection<DbQuestion> GetQuizQuestions(int quizId)
        {
            return _context.DbQuestions.Where(x => x.QuizId == quizId).Include(x => x.Answers).ToList();
        }

        public ICollection<DbAnswer> GetQuestionAnswers(int questionId)
        {
            return answerRepository.FindByConditionAsync(a => a.QuestionId == questionId).Result;
        }
    }
}
