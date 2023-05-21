using CommonLibrary.LibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services;

internal interface IQuizResultService
{
    public ICollection<Quiz> GetAll();
    public bool Add(QuizResult quizResult);
}
