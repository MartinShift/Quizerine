using CommonLibrary.LibraryModels;
using My.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Client.Windows;
using System.Windows.Media.Imaging;
using Client_Wpf.Models;

namespace Client.ViewModels;

public class MainWindowViewModel : NotifyPropertyChangedBase
{
    public MainWindowViewModel()
    {
        _allQuizzes = new List<Quiz>();
        _nickname = string.Empty;
        _allQuizResults = new List<QuizResult>(); 

        // TESTING
        Answer answ1 = new Answer()
        {
            Id = 0,
            Text = "Wrong answer #1",
            IsCorrect = false,
            IsSelected = false
        };
        Answer answ2 = new Answer()
        {
            Id = 1,
            Text = "Wrong answer #2",
            IsCorrect = false,
            IsSelected = false
        };
        Answer answ3 = new Answer()
        {
            Id = 2,
            Text = "Correct answer",
            IsCorrect = true,
            IsSelected = false
        };
        Answer answ4 = new Answer()
        {
            Id = 3,
            Text = "Wrong answer #3",
            IsCorrect = false,
            IsSelected = false
        };
        var answers = new List<Answer>() {answ1, answ2, answ3, answ4 };
        BitmapImage image1 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\Q1.jpg"));
        BitmapImage image2 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\Q2.jpg"));
        BitmapImage image3 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\Q3.jpg"));

        Question quest1 = new Question()
        {
            Id = 0,
            Text = "Question 1",
            Answers = answers,
            Image = Helper.ImageToBytes(image1)
        };
        Question quest2 = new Question()
        {
            Id = 1,
            Text = "Question 2",
            Answers = answers,
            Image = Helper.ImageToBytes(image2)
        };
        Question quest3 = new Question()
        {
            Id = 2,
            Text = "Question 3",
            Answers = answers,
            Image = Helper.ImageToBytes(image3)
        };
        var questions = new List<Question>() { quest1, quest2, quest3 };
        BitmapImage quizImage1 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\quiz1.jpg"));
        BitmapImage quizImage2 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\quiz2.png"));
        BitmapImage quizImage3 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\quiz3.png"));
        Quiz q1 = new Quiz()
        {
            Id = 0,
            Image = Helper.ImageToBytes(quizImage1),
            TimeLimit = 30,
            Title = "Test #1",
            Questions = questions
        };
        Quiz q2 = new Quiz()
        {
            Id = 1,
            Image = Helper.ImageToBytes(quizImage2),
            TimeLimit = 60,
            Title = "Test #2",
            Questions = questions
        };
        Quiz q3 = new Quiz()
        {
            Id = 2,
            Image = Helper.ImageToBytes(quizImage3),
            TimeLimit = 90,
            Title = "Test #3",
            Questions = questions
        };

        _allQuizzes.Add(q1);
        _allQuizzes.Add(q2);
        _allQuizzes.Add(q3);


        QuizResult result1 = new QuizResult
        {
            ClientName = "user1",
            Points = 12,
            Quiz = q1,
            SecondsSpent = 33
        };
        QuizResult result2 = new QuizResult
        {
            ClientName = "mike",
            Points = 22,
            Quiz = q1,
            SecondsSpent = 41
        };
        QuizResult result3 = new QuizResult
        {
            ClientName = "player_01",
            Points = 27,
            Quiz = q1,
            SecondsSpent = 53
        };
        QuizResult result4 = new QuizResult
        {
            ClientName = "Corey",
            Points = 19,
            Quiz = q1,
            SecondsSpent = 23
        };
        QuizResult result5 = new QuizResult
        {
            ClientName = "Corey",
            Points = 29,
            Quiz = q2,
            SecondsSpent = 40
        };

        _allQuizResults.Add(result1);
        _allQuizResults.Add(result2);
        _allQuizResults.Add(result3);
        _allQuizResults.Add(result4);
        _allQuizResults.Add(result5);



        _selectedQuiz = new QuizViewModel(q1); // will be firstordefault from db

        // END TESTING
        // load all Quiz
        // load all QuizResult

      
        //_loadQuizzes();
        //_loadQuizResults();
    }
    private void _loadQuizzes()
    {
        _allQuizzes = Client.Models.ServerHelper.GetQuizzes();
    }
    private void _loadQuizResults()
    {
        //_allQuizResults = ;
    }
    private List<Quiz> _allQuizzes;
    public ObservableCollection<QuizViewModel> Quizzes
    {
       get
       {
            var collection = new ObservableCollection<QuizViewModel>();
            _allQuizzes.ForEach(q => collection.Add(new QuizViewModel(q)));
            return collection;
       }
       set
       {
            Quizzes = value;
            OnPropertyChanged(nameof(Quizzes));
       }
    }
    private QuizViewModel _selectedQuiz;
    public QuizViewModel SelectedQuiz
    {
        get => _selectedQuiz;
        set
        {
            _selectedQuiz = value;
            OnPropertyChanged(nameof(SelectedQuiz));
            OnPropertyChanged(nameof(QuizResults));
        }
    }
    private List<QuizResult> _allQuizResults;
    public ObservableCollection<QuizResultViewModel> QuizResults
    {
        get
        {
            var collection = new ObservableCollection<QuizResultViewModel>();
            var sorted = _allQuizResults.Where(qr => qr.Quiz.Id == _selectedQuiz.Id).OrderByDescending(x => x.Points).ToList();
            int pos = 1;
            sorted.ForEach(qr =>
            {
                collection.Add(new QuizResultViewModel(qr, pos));
                pos++;
            });
            return collection;
        }
        set
        {
            QuizResults = value;
            OnPropertyChanged(nameof(QuizResults));
        }
    }
    private QuizResultViewModel _selectedQuizResult;
    public QuizResultViewModel SelectedQuizResult
    {
        get => _selectedQuizResult;
        set
        {
            _selectedQuizResult = value;
            OnPropertyChanged(nameof(SelectedQuizResult));
        }
    }
    private string _nickname;
    public string Nickname
    {
        get => _nickname;
        set
        {
            _nickname = value; 
            OnPropertyChanged(nameof(Nickname));
        }
    }
    public ICommand TakeQuizCommand => new RelayCommand(x =>
    {
        //MessageBox.Show($"Selected quiz: {_selectedQuiz.Title}\nNickName: {_nickname}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        // open new quiz window
        Application.Current.MainWindow.Hide();
        var window = new QuizWindow(_selectedQuiz, Nickname);
        window.Show();
        // update QuizResults
        //_loadQuizResults();
    }, x => _selectedQuiz !=null && _nickname.Length>0);
}
