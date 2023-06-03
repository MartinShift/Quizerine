using CommonLibrary.LibraryModels;
using Server.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services.Interfaces;

public interface IQuizResultService
{
    public void Load(QuizResult quizResult);
    public bool HasPassed(string client, Quiz quiz);
    public ICollection<DbQuizResult> GetAll();
    public ICollection<DbQuizResult> FindResultsByClient(string client);
    public ICollection<string> FindClientsByQuiz(int quizId);
}
