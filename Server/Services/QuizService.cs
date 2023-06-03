using CommonLibrary.LibraryModels;
using DbModels.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Server.DbModels;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    public class QuizService : IQuizService
    {
        Repository<DbQuiz> quizRepository { get; set; }
        Repository<DbQuestion> questionRepository { get; set; }
        Repository<DbAnswer> answerRepository { get; set; }
        QuizerineDbContext _context { get; set; }
        public QuizService(QuizerineDbContext quizerineDb)
        {
            quizRepository = new Repository<DbQuiz>(quizerineDb);
            answerRepository = new Repository<DbAnswer>(quizerineDb);
            questionRepository = new Repository<DbQuestion>(quizerineDb);
            _context = quizerineDb;
        }
        public void Add(Quiz quiz)
        {
            var dbquiz = new DbQuiz()
            {
                Title = quiz.Title,
                Image = quiz.Image,
                TimeLimit = quiz.TimeLimit,
            };
            quizRepository.AddAsync(dbquiz).Wait();
            try
            {
                SaveChanges();
            }
            catch (DbException)
            {
                throw new Exception("Помилка під час спроби додати вікторину");
            }
        }

        // new quiz - new question - new answers
        // exist quize - exist+new question - exist+new answer
        public void Load(Quiz quiz)
        {

            DbQuiz dbQuiz;
            
            if (quiz.Id == null)
            {
                // no quiz id - new quiz
                dbQuiz = new DbQuiz
                {
                    Title = quiz.Title,
                    TimeLimit = quiz.TimeLimit,
                    Image = quiz.Image
                };
                quiz.Questions.ForEach(q =>
                {
                    var dbQuestion = new DbQuestion()
                    {
                        Text = q.Text,
                    };
                    dbQuiz.Questions.Add(dbQuestion);
                    q.Answers.ForEach(a => {
                        var dbAnswer = new DbAnswer()
                        {
                            IsCorrect = a.IsCorrect,
                            Text = a.Text
                        };
                        dbQuestion.Answers.Add(dbAnswer);
                    });

                });
                _context.Add(dbQuiz);
            }
            else
            {
                // has quiz id - exist quiz
                dbQuiz = _context.DbQuizzes
                    .Include(q => q.Questions) 
                    .ThenInclude(q => q.Answers)    
                    .First(x => x.Id == quiz.Id);
                
                dbQuiz.Title = quiz.Title;
                dbQuiz.TimeLimit = quiz.TimeLimit;

                quiz.Questions.ForEach(q =>
                {
                    DbQuestion? dbQuestion = null;
                    if (q.Id != null)
                    {
                        dbQuestion = dbQuiz.Questions.FirstOrDefault(x => x.Id == q.Id);
                    }
                    if (dbQuestion == null)
                    {
                        dbQuestion = new DbQuestion();
                        dbQuiz.Questions.Add(dbQuestion);
                    }
                    dbQuestion.Text = q.Text;

                    q.Answers.ForEach(a =>
                    {
                        DbAnswer? dbAnswer = null;
                        if (a.Id != null)
                        {
                            dbAnswer = dbQuestion.Answers.FirstOrDefault(x => x.Id == a.Id);
                        }
                        if (dbAnswer == null)
                        {
                            dbAnswer = new DbAnswer();
                            dbQuestion.Answers.Add(dbAnswer);
                        }
                        dbAnswer.Text = a.Text;
                        dbAnswer.IsCorrect = a.IsCorrect;
                    });
                });

            }

            _context.SaveChanges();

        }

        public void Add(Quiz quiz, Question question)
        {
            var dbquestion = new DbQuestion()
            {
                Text = question.Text,
                Image = question.Image,
                //QuizId = (int)quiz.Id,
            };
            questionRepository.AddAsync(dbquestion).Wait();
            try
            {
                SaveChanges();
            }
            catch (DbException e)
            {
                throw new Exception("Помилка під час спроби додати питання до вікторини");
            }
        }
        public void Add(int quizId, Question question)
        {
            var dbquestion = new DbQuestion()
            {
                //Id = question.Id,
                Text = question.Text,
                Image = question.Image,
                QuizId = quizId,
            };
            questionRepository.AddAsync(dbquestion).Wait();
            try
            {
                SaveChanges();
            }
            catch (DbException e)
            {
                throw new Exception("Помилка під час спроби додати питання до вікторини");
            }
        }

        public void Add(int quizId, Question question, Answer answer)
        {
            var dbquestion = new DbQuestion()
            {
                //Id = question.Id,
                Text = question.Text,
                Image = question.Image,
                QuizId = quizId,
            };
            questionRepository.AddAsync(dbquestion).Wait();
            var dbanswer = new DbAnswer()
            {
                //Id = answer.Id,
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
            };
            answerRepository.AddAsync(dbanswer).Wait();
            try
            {
                SaveChanges();
            }
            catch (DbException e)
            {
                throw new Exception("Помилка під час спроби додати питання та відповіді до вікторини");
            }
        }

        public void Delete(Quiz quiz)
        {
            quizRepository.DeleteAsync((int)quiz.Id).Wait();
            try
            {
                SaveChanges();
            }
            catch (DbException e)
            {
                throw new Exception("Помилка під час спроби видалити вікторину");
            }
        }

        public void Delete(Question question)
        {
            questionRepository.DeleteAsync((int)question.Id).Wait();
            try
            {
                SaveChanges();
            }
            catch (DbException e)
            {
                throw new Exception("Помилка під час спроби видалити питання");
            }
        }

        public void Delete(Answer answer)
        {
            //answerRepository.DeleteAsync(answer.Id).Wait();
            try
            {
                SaveChanges();
            }
            catch (DbException e)
            {
                throw new Exception("Помилка під час спроби видалити відповідь до питання");
            }
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
            try
            {
                SaveChanges();
            }
            catch (DbException e)
            {
                throw new Exception("Помилка під час спроби оновити вікторину");
            }
        }

        public void Update(Question question)
        {
            var dbquestion = new DbQuestion()
            {
                Text = question.Text,
                Image = question.Image,
            };
            questionRepository.UpdateAsync(dbquestion).Wait();
            try
            {
                SaveChanges();
            }
            catch (DbException e)
            {
                throw new Exception("Помилка під час спроби оновити питання до вікторини");
            }
        }

        public void Update(Answer answer)
        {
            var dbanswer = new DbAnswer()
            {
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
            };
            answerRepository.UpdateAsync(dbanswer).Wait();
            try
            {
                SaveChanges();
            }
            catch (DbException e)
            {
                throw new Exception("Помилка під час спроби оновити відповідь на питання до вікторини");
            }
        }

        public void Add(int questionId, Answer answer)
        {
            var dbanswer = new DbAnswer()
            {
                Text = answer.Text,
                IsCorrect = answer.IsCorrect,
                Question = _context.DbQuestions.First(x => x.Id == questionId),
            };
            answerRepository.AddAsync(dbanswer).Wait();
            try
            {
                SaveChanges();
            }
            catch (DbException e)
            {
                throw new Exception("Помилка під час спроби додати відповідь до питання до вікторини");
            }
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
            return _context.DbQuizzes.Include(x => x.Results).Include(x => x.Questions).ThenInclude(x => x.Answers).ToList();
        }

        public ICollection<DbQuestion> GetQuizQuestions(int quizId)
        {
            return _context.DbQuestions.Where(x => x.QuizId == quizId).Include(x => x.Answers).ToList();
        }

        public ICollection<DbAnswer> GetQuestionAnswers(int questionId)
        {
            return answerRepository.FindByConditionAsync(a => a.Question.Id == questionId).Result;
        }
        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int GetQUizId(Quiz quiz)
        {
            return quizRepository.FindByConditionAsync(x => x.Title == quiz.Title).Result.First().Id;
        }
    }
}
