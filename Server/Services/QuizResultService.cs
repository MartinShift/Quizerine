using CommonLibrary.LibraryModels;
using DbModels.Repository.Interfaces;
using Server.DbModels;
using Server.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services
{
    internal class QuizResultService : IQuizResultService
    {
        Repository<DbQuizResult> quizResultRepository { get; set; }
        QuizerineDbContext _context;
        public QuizResultService(QuizerineDbContext context)
        {
            _context = context;
            quizResultRepository = new Repository<DbQuizResult>(context);
        }
        public void Add(QuizResult quizResult)
        {
            var dbquizResult = new DbQuizResult
            {
                ClientName = quizResult.ClientName,
                SecondsSpent = quizResult.SecondsSpent,
                Points = quizResult.Points,
                QuizId = quizResult.Quiz.Id,
            }; quizResultRepository.AddAsync(dbquizResult).Wait();
            try
            {
                _context.SaveChanges();
            }
            catch (DbException)
            {
                throw new Exception("Помилка під час спроби додати результат вікторини");
            }
        }

        public bool HasPassed(string client, Quiz quiz) => quizResultRepository.ExistsAsync(qr => qr.ClientName == client && qr.Quiz.Id == quiz.Id).Result;

        public ICollection<DbQuizResult> FindResultsByClient(string client)
        {
            return quizResultRepository.FindByConditionAsync(x => x.ClientName == client).Result;
        }
        public ICollection<DbQuizResult> GetAll()
        {
            return quizResultRepository.GetAllAsync().Result;
        }
        public ICollection<string> FindClientsByQuiz(int quizId)
        {
            return quizResultRepository.FindByConditionAsync(x => x.QuizId == quizId).Result.Select(x => x.ClientName).ToList();
        }
    }
}
