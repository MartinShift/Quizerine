using CommonLibrary.LibraryModels;
using Server.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Server.Services;

public interface IQuizService
{
    public ICollection<DbQuiz> GetAll();

    public void Load(Quiz quiz);

    public void Add(Quiz quiz);
    public void Update(Quiz quiz);
    public void Delete(Quiz quiz);

    public void Add(int quizId, Question question);

    public void Add(Quiz quiz, Question question);
    public void Update(Question question);
    public void Delete(Question question);

    public void Add(int questionId, Answer answer);
    public void Update(Answer answer);
    public void Delete(Answer answer);
    public int GetQUizId(Quiz quiz);
    public ICollection<DbQuiz> FindQuizByCondition(Expression<Func<DbQuiz, bool>> expression);
    public ICollection<DbQuestion> FindQuestionByCondition(Expression<Func<DbQuestion, bool>> expression);
    public ICollection<DbAnswer> FindAnswerByCondition(Expression<Func<DbAnswer, bool>> expression);
    public ICollection<DbQuestion> GetQuizQuestions(int quizId);
    public ICollection<DbAnswer> GetQuestionAnswers(int questionId);
}