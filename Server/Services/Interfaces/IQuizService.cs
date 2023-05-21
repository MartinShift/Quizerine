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
    public DbQuiz FindQuizByCondition(Expression<Func<DbQuiz, bool>> expression);
    public DbQuestion FindQuestionByCondition(Expression<Func<DbQuestion, bool>> expression);
    public DbAnswer FindQuestionByCondition(Expression<Func<DbAnswer, bool>> expression);
    public DbQuestion GetQuizQuestion(int quizId);
    public ICollection<DbAnswer> GetQuestionAnswers(int questionId);
}