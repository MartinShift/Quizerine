using CommonLibrary.LibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services;

public interface IQuizService
{
    public ICollection<Quiz> GetAll();

    public bool Add(Quiz quiz);
    public bool Update(Quiz quiz);
    public bool Delete(Quiz quiz);

    public bool Add(Quiz quiz, Question question);
    public bool Update(Question question);
    public bool Delete(Question question);

    public bool Add(Question question, Answer answer);
    public bool Update(Answer answer);
    public bool Delete(Answer answer);
}
