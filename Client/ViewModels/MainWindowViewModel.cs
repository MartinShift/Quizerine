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

namespace Client.ViewModels;

public class MainWindowViewModel : NotifyPropertyChangedBase
{
    public MainWindowViewModel()
    {
        _allQuizzes = new List<Quiz>();
        _nickname = string.Empty;

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

        Question quest1 = new Question()
        {
            Id = 0,
            Text = "Question 1",
            Answers = answers
        };
        Question quest2 = new Question()
        {
            Id = 1,
            Text = "Question 2",
            Answers = answers
        };
        Question quest3 = new Question()
        {
            Id = 2,
            Text = "Question 3",
            Answers = answers
        };
        var questions = new List<Question>() { quest1, quest2, quest3 };

        Quiz q1 = new Quiz()
        {
            Id = 0,
            Image = null,
            TimeLimit = 30,
            Title = "Test #1",
            Questions = questions
        };
        Quiz q2 = new Quiz()
        {
            Id = 1,
            Image = null,
            TimeLimit = 60,
            Title = "Test #2",
            Questions = questions
        };
        Quiz q3 = new Quiz()
        {
            Id = 2,
            Image = null,
            TimeLimit = 90,
            Title = "Test #3",
            Questions = questions
        };

        _allQuizzes.Add(q1);
        _allQuizzes.Add(q2);
        _allQuizzes.Add(q3);


        // END TESTING


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
        var window = new QuizWindow(_selectedQuiz);
        window.Show();

    }, x => _selectedQuiz !=null && _nickname.Length>0);
}
