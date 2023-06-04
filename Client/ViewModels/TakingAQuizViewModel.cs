using Client.Models;
using Client_Wpf.Models;
using CommonLibrary.LibraryModels;
using My.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Client.ViewModels;

public class TakingAQuizViewModel : NotifyPropertyChangedBase
{
    public TakingAQuizViewModel(QuizViewModel quiz, string nickname, ServerHelper helper)
    {
        _server = helper;
        _quiz = quiz;
        _nickname = nickname;
        _currentQuestionIndex = 0;
        //_currentQuestion = _quiz.Questions[_currentQuestionIndex];
        _answers = new List<bool>();
        _time = TimeSpan.FromSeconds(_quiz.TimeLimit);

        _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
        {
            _secondsLeft = _time.ToString("c");
            if (_time == TimeSpan.Zero)
            {
                _timer.Stop();
                _secondsLeft = _time.ToString("c");
                OnPropertyChanged(nameof(SecondsLeft));
                MessageBox.Show($"Time's up/\nYou did not make it untill the end", "Sorry!", MessageBoxButton.OK, MessageBoxImage.Information);
                CloseQuiz();
            }
            _time = _time.Add(TimeSpan.FromSeconds(-1));
            OnPropertyChanged(nameof(SecondsLeft));
        }, Application.Current.Dispatcher);

        _currentQuestion = _quiz.Questions[_currentQuestionIndex];
        _timer.Start();
    }
    private readonly ServerHelper _server;
    private string _nickname;
    private QuizViewModel _quiz;
    private QuestionViewModel _currentQuestion;
    private DispatcherTimer _timer;
    private TimeSpan _time;

    //private string _currentQuestionText;
    public string CurrentQuestionText
    {
        get => _currentQuestion.Text;
    }
    public string AnswerAText
    {
        get => _currentQuestion.Answers[0].Text;
    }
    public string AnswerBText
    {
        get => _currentQuestion.Answers[1].Text;
    }
    public string AnswerCText
    {
        get => _currentQuestion.Answers[2].Text;
    }
    public string AnswerDText
    {
        get => _currentQuestion.Answers[3].Text;
    }
    private int _currentQuestionIndex;
    public int CurrentQuestionNumber
    {
        //get => _quiz.Questions.IndexOf(_currentQuestion) + 1;
        get => _currentQuestionIndex + 1;
    }
    public int AllQuestionsCount
    {
        get => _quiz.Questions.Count;
    }
    private string _secondsLeft;
    public string SecondsLeft
    {
        get => _secondsLeft;
    }
    private List<bool> _answers;
    public BitmapImage QuestionImage
    {
        get
        {
            var image = new BitmapImage();
            if(_currentQuestion.Image !=null)
            {
                return Helper.ImageFromBytes(_currentQuestion.Image);
            }
            return image;
        }
    }
    public ICommand AnswerACommand => new RelayCommand(x =>
    {
        // register answer
        _answers.Add(_currentQuestion.Answers[0].IsCorrect);
        MoveToNextQuestion();
    }, x => true);
    public ICommand AnswerBCommand => new RelayCommand(x =>
    {
        // register answer
        _answers.Add(_currentQuestion.Answers[1].IsCorrect);
        MoveToNextQuestion();
    }, x => true);
    public ICommand AnswerCCommand => new RelayCommand(x =>
    {
        // register answer
        _answers.Add(_currentQuestion.Answers[2].IsCorrect);
        MoveToNextQuestion();
    }, x => true);
    public ICommand AnswerDCommand => new RelayCommand(x =>
    {
        // register answer
        _answers.Add(_currentQuestion.Answers[3].IsCorrect);
        MoveToNextQuestion();
    }, x => true);
    public void MoveToNextQuestion()
    {
        try
        {
            _currentQuestionIndex++;
            if(_currentQuestionIndex > AllQuestionsCount-1)   //last question
            {
                //stop timer
                _timer.Stop();
                _secondsLeft = _time.ToString("c");
                OnPropertyChanged(nameof(SecondsLeft));
                var timeElapsed = _quiz.TimeLimit - _time.TotalSeconds;
                int correctAnswersCount = _answers.Where(a => a).Count();
                var score = GetScore(timeElapsed, correctAnswersCount);
                //MessageBox.Show($"Quiz finished\nYour result is {correctAnswersCount} out of {AllQuestionsCount}\nIt took you {_quiz.TimeLimit - _time.TotalSeconds} seconds", "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBox.Show($"Quiz finished\nYour result is {correctAnswersCount} out of {AllQuestionsCount}\nIt took you {timeElapsed} seconds.\nYOUR SCORE IS {score}", "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Information);

                var result = new QuizResult()
                {
                    ClientName = _nickname, 
                    Quiz = _quiz.Model,
                    SecondsSpent = (int)timeElapsed,
                    Points = score
                };
                
                //Client.Models.ServerHelper.SendQuizResult(result);
                _server.SendQuizResult(result);
                CloseQuiz();

                return;
            }
            _currentQuestion = _quiz.Questions[_currentQuestionIndex];
            OnPropertyChanged(nameof(CurrentQuestionText));
            OnPropertyChanged(nameof(AnswerAText));
            OnPropertyChanged(nameof(AnswerBText));
            OnPropertyChanged(nameof(AnswerCText));
            OnPropertyChanged(nameof(AnswerDText));
            OnPropertyChanged(nameof(CurrentQuestionNumber));
            OnPropertyChanged(nameof(QuestionImage));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    public int GetScore(double timeElapsed, int correctAnswersCount)
    {
        int result = 0;

        if(correctAnswersCount==0)
        {
            return 0;
        }

        // LOGIC Let's say each correct answer = 30 points. Each unused second = 2 points.

        // 5/5 correctsAnswers for 30/60 seconds = (5*30) + ((60-30)*2) = 210 points
        // 3/5 correctsAnswers for 30/60 seconds = (3*30) + ((60-30)*2) = 150 points
        // 1/5 correctsAnswers for 30/60 seconds = (1*30) + ((60-30)*2) = 90 points

        // 5/5 correctsAnswers for 50/60 seconds = (5*30) + ((60-50)*2) = 170 points
        // 3/5 correctsAnswers for 50/60 seconds = (3*30) + ((60-50)*2) = 110 points
        // 1/5 correctsAnswers for 50/60 seconds = (1*30) + ((60-50)*2) = 50 points

        //int pointForCorrectsAnswer = 30;
        //int pointForUnusedSecond = 2;

        //var correctAnswersPoints = correctAnswersCount * pointForCorrectsAnswer;
        //var unusedTimePoints = (_quiz.TimeLimit - (int)timeElapsed) * pointForUnusedSecond;

        //result = correctAnswersPoints + unusedTimePoints;
        var calculator2 = new PointCalculatorVer02();
        var result2 = calculator2.CalculatePoints(_quiz.TimeLimit, (int)timeElapsed, _quiz.Questions.Count, correctAnswersCount);
        return result2;
    }
    public void CloseQuiz()
    {
        var thisWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        thisWindow.Close();
        Application.Current.MainWindow.Show();
    }
}
