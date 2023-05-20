using CommonLibrary.LibraryModels;
using My.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Client.ViewModels;

public class TakingAQuizViewModel : NotifyPropertyChangedBase
{
    public TakingAQuizViewModel(QuizViewModel quiz)
    {
        _quiz = quiz;
        _currentQuestionIndex = 0;
        //_currentQuestion = _quiz.Questions[_currentQuestionIndex];
        _answers = new List<bool>();
        _time = TimeSpan.FromSeconds(_quiz.TimeLimit);

        _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
        {
            _secondsLeft = _time.ToString("c");
            if (_time == TimeSpan.Zero) _timer.Stop();
            _time = _time.Add(TimeSpan.FromSeconds(-1));
            OnPropertyChanged(nameof(SecondsLeft));
        }, Application.Current.Dispatcher);

        _currentQuestion = _quiz.Questions[_currentQuestionIndex];
        _timer.Start();
    }
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
                //MessageBox.Show($"Quiz finished\nYour result is {correctAnswersCount} out of {AllQuestionsCount}\nIt took you {_quiz.TimeLimit - _time.TotalSeconds} seconds", "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBox.Show($"Quiz finished\nYour result is {correctAnswersCount} out of {AllQuestionsCount}\nIt took you {timeElapsed} seconds", "Congratulations!", MessageBoxButton.OK, MessageBoxImage.Information);
                //close window and show main
                var thisWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                thisWindow.Close();
                Application.Current.MainWindow.Show();
                return;
            }
            _currentQuestion = _quiz.Questions[_currentQuestionIndex];
            OnPropertyChanged(nameof(CurrentQuestionText));
            OnPropertyChanged(nameof(AnswerAText));
            OnPropertyChanged(nameof(AnswerBText));
            OnPropertyChanged(nameof(AnswerCText));
            OnPropertyChanged(nameof(AnswerDText));
            OnPropertyChanged(nameof(CurrentQuestionNumber));

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
