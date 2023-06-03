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
using Client.Models;

namespace Client.ViewModels;

public class MainWindowViewModel : NotifyPropertyChangedBase
{
    public MainWindowViewModel()
    {
        _allQuizzes = new List<Quiz>();
        _nickname = string.Empty;
        _allQuizResults = new List<QuizResult>();

        // TESTING
        //Answer answ1 = new Answer()
        //{
        //    Id = 0,
        //    Text = "Wrong answer #1",
        //    IsCorrect = false,
        //    IsSelected = false
        //};
        //Answer answ2 = new Answer()
        //{
        //    Id = 1,
        //    Text = "Wrong answer #2",
        //    IsCorrect = false,
        //    IsSelected = false
        //};
        //Answer answ3 = new Answer()
        //{
        //    Id = 2,
        //    Text = "Correct answer",
        //    IsCorrect = true,
        //    IsSelected = false
        //};
        //Answer answ4 = new Answer()
        //{
        //    Id = 3,
        //    Text = "Wrong answer #3",
        //    IsCorrect = false,
        //    IsSelected = false
        //};
        //var answers = new List<Answer>() {answ1, answ2, answ3, answ4 };
        //BitmapImage image1 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\Q1.jpg"));
        //BitmapImage image2 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\Q2.jpg"));
        //BitmapImage image3 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\Q3.jpg"));

        //Question quest1 = new Question()
        //{
        //    Id = 0,
        //    Text = "Question 1",
        //    Answers = answers,
        //    Image = Helper.ImageToBytes(image1)
        //};
        //Question quest2 = new Question()
        //{
        //    Id = 1,
        //    Text = "Question 2",
        //    Answers = answers,
        //    Image = Helper.ImageToBytes(image2)
        //};
        //Question quest3 = new Question()
        //{
        //    Id = 2,
        //    Text = "Question 3",
        //    Answers = answers,
        //    Image = Helper.ImageToBytes(image3)
        //};
        //var questions = new List<Question>() { quest1, quest2, quest3 };
        //BitmapImage quizImage1 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\quiz1.jpg"));
        //BitmapImage quizImage2 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\quiz2.png"));
        //BitmapImage quizImage3 = new BitmapImage(new Uri("C:\\Users\\user\\source\\repos\\Quizerine\\Client\\quiz3.png"));
        //Quiz q1 = new Quiz()
        //{
        //    Id = 0,
        //    Image = Helper.ImageToBytes(quizImage1),
        //    TimeLimit = 30,
        //    Title = "Test #1",
        //    Questions = questions
        //};
        //Quiz q2 = new Quiz()
        //{
        //    Id = 1,
        //    Image = Helper.ImageToBytes(quizImage2),
        //    TimeLimit = 60,
        //    Title = "Test #2",
        //    Questions = questions
        //};
        //Quiz q3 = new Quiz()
        //{
        //    Id = 2,
        //    Image = Helper.ImageToBytes(quizImage3),
        //    TimeLimit = 90,
        //    Title = "Test #3",
        //    Questions = questions
        //};

        //_allQuizzes.Add(q1);
        //_allQuizzes.Add(q2);
        //_allQuizzes.Add(q3);


        //QuizResult result1 = new QuizResult
        //{
        //    ClientName = "user1",
        //    Points = 12,
        //    Quiz = q1,
        //    SecondsSpent = 33
        //};
        //QuizResult result2 = new QuizResult
        //{
        //    ClientName = "mike",
        //    Points = 22,
        //    Quiz = q1,
        //    SecondsSpent = 41
        //};
        //QuizResult result3 = new QuizResult
        //{
        //    ClientName = "player_01",
        //    Points = 27,
        //    Quiz = q1,
        //    SecondsSpent = 53
        //};
        //QuizResult result4 = new QuizResult
        //{
        //    ClientName = "Corey",
        //    Points = 19,
        //    Quiz = q1,
        //    SecondsSpent = 23
        //};
        //QuizResult result5 = new QuizResult
        //{
        //    ClientName = "Corey",
        //    Points = 29,
        //    Quiz = q2,
        //    SecondsSpent = 40
        //};
        //QuizResult result6 = new QuizResult
        //{
        //    ClientName = "DwightSchrute",
        //    Points = 41,
        //    Quiz = q1,
        //    SecondsSpent = 37
        //};
        //QuizResult result7 = new QuizResult
        //{
        //    ClientName = "Vessel",
        //    Points = 39,
        //    Quiz = q1,
        //    SecondsSpent = 43
        //};
        //QuizResult result8 = new QuizResult
        //{
        //    ClientName = "DwightSchrute",
        //    Points = 32,
        //    Quiz = q2,
        //    SecondsSpent = 38
        //};
        //QuizResult result9 = new QuizResult
        //{
        //    ClientName = "mike",
        //    Points = 28,
        //    Quiz = q3,
        //    SecondsSpent = 36
        //};
        //QuizResult result10 = new QuizResult
        //{
        //    ClientName = "keychron",
        //    Points = 38,
        //    Quiz = q1,
        //    SecondsSpent = 47
        //};

        //_allQuizResults.Add(result1);
        //_allQuizResults.Add(result2);
        //_allQuizResults.Add(result3);
        //_allQuizResults.Add(result4);
        //_allQuizResults.Add(result5);
        //_allQuizResults.Add(result6);
        //_allQuizResults.Add(result7);
        //_allQuizResults.Add(result8);
        //_allQuizResults.Add(result9);
        //_allQuizResults.Add(result10);



        //_selectedQuizForResults = new QuizViewModel(q1); // will be firstordefault from db

        // END TESTING
        // load all Quiz
        // load all QuizResult

        _server = new ServerHelper("127.0.0.1", 5000);
        _loadQuizzes();
        _loadQuizResults();    
    }
    private readonly ServerHelper _server;
    private void _loadQuizzes()
    {
        _allQuizzes.Clear();
        _allQuizzes.AddRange(_server.GetQuizzes());
    }
    private void _loadQuizResults()
    {
        _allQuizResults.Clear();
        _allQuizResults.AddRange(_server.GetQuizResults());
        _selectedQuizForResults = new QuizViewModel(_allQuizzes.FirstOrDefault());
    }
    private List<Quiz> _allQuizzes;
    public ObservableCollection<QuizViewModel> Quizzes   // quizzes filtered for user (nickname)
    {
       get
       {
            var collection = new ObservableCollection<QuizViewModel>();
            _allQuizzes.ForEach(q =>
            {
                if (_allQuizResults != null)
                {
                    if (_allQuizResults.Where(qr => qr.Quiz.Id==q.Id).FirstOrDefault(x => x.ClientName == _nickname) == null)
                    {
                        collection.Add(new QuizViewModel(q));
                    }
                }
            });
            return collection;
        }
       set
       {
            Quizzes = value;
            OnPropertyChanged(nameof(Quizzes));
       }
    }
    public ObservableCollection<QuizViewModel> QuizzesForResults    // all quizzes
    {
        get
        {
            var collection = new ObservableCollection<QuizViewModel>();
            _allQuizzes.ForEach(q =>
            {
                collection.Add(new QuizViewModel(q));
            });
            return collection;
        }
        set
        {
            QuizzesForResults = value;
            OnPropertyChanged(nameof(QuizzesForResults));
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
            //OnPropertyChanged(nameof(QuizResults));
        }
    }
    private QuizViewModel _selectedQuizForResults;
    public QuizViewModel SelectedQuizForResults
    {
        get => _selectedQuizForResults;
        set
        {
            _selectedQuizForResults = value;
            OnPropertyChanged(nameof(SelectedQuizForResults));
            OnPropertyChanged(nameof(QuizResults));
        }
    }
    private List<QuizResult> _allQuizResults;
    public ObservableCollection<QuizResultViewModel> QuizResults
    {
        get
        {
            var collection = new ObservableCollection<QuizResultViewModel>();
            if(_selectedQuizForResults == null)
            {
                _selectedQuizForResults = new QuizViewModel(_allQuizzes.FirstOrDefault());
            }
            var sorted = _allQuizResults.Where(qr => qr.Quiz.Id == _selectedQuizForResults.Id).OrderByDescending(x => x.Points).ToList();
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
            OnPropertyChanged(nameof(Quizzes));
        }
    }
    //public ICommand CheckNicknameCommand => new RelayCommand(x =>
    //{
    //    if(_allQuizResults.FirstOrDefault(x => x.ClientName == _nickname) == null)
    //    {
    //        // new user => all quizzes are avilable
    //    }
    //    else
    //    {
    //        // filter Quizzes
    //        OnPropertyChanged(nameof(Quizzes));
    //    }
    //}, x => _nickname.Length > 0);
    public ICommand TakeQuizCommand => new RelayCommand(x =>
    {
        //MessageBox.Show($"Selected quiz: {_selectedQuiz.Title}\nNickName: {_nickname}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        // open new quiz window
        Application.Current.MainWindow.Hide();
        var window = new QuizWindow(_selectedQuiz, Nickname, _server);
        window.ShowDialog();
        // update QuizResults
        _loadQuizResults();
        OnPropertyChanged(nameof(QuizResults));
    }, x => _selectedQuiz !=null && _nickname.Length>0);
}
