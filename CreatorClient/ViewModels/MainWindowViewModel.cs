using Client.Models;
using CommonLibrary.LibraryModels;
using My.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CreatorClient.ViewModels;

public class MainWindowViewModel : NotifyPropertyChangedBase
{
    public MainWindowViewModel()
    {
        _quizes = new();
        

        //Task.Run(() => { LoadQuizes(); });

    }
    private List<Quiz> _quizes { get; set; }
    private void LoadQuizes()
    {
        _quizes.AddRange(ServerHelper.GetQuizzes());
        //TODO Get from server
        //_quizes.Add(new Quiz
        //{
        //    Id = 1,
        //    Title = "Quiz # 1",
        //    TimeLimit = 90,
        //});

        //_quizes.Add(new Quiz
        //{
        //    Id = 2,
        //    Title = "Quiz # 2",
        //    TimeLimit = 60,
        //});
        //_quizes.Add(new Quiz
        //{
        //    Id = 3,
        //    Title = "Quiz # 3",
        //    TimeLimit = 120,
        //});

        OnPropertyChanged(nameof(Quizes));
    }




    public ObservableCollection<QuizViewModel> Quizes
    {
        get
        {
            var collection = new ObservableCollection<QuizViewModel>();
            _quizes.ForEach(x => collection.Add(new QuizViewModel(x)));
            return collection;

        }
    }

    private QuizViewModel _selectedQuiz;
    public QuizViewModel SelectedQuiz
    {
        get => _selectedQuiz; set
        {
            _selectedQuiz = value;
            OnPropertyChanged(nameof(SelectedQuiz));
            ;
        }

    }
    public ICommand CreateQuiz => new RelayCommand(x =>
    {
        _quizes.Add(new Quiz
        {
            Title = "New quizz",
            TimeLimit = 60,
        });
        OnPropertyChanged(nameof(Quizes));


    }, x => true);

    public ICommand EditQuiz => new RelayCommand(x =>
    {

        var window = new CreateQuiz(SelectedQuiz);
        window.ShowDialog();


    }, x => true);

    public ICommand SaveToServer => new RelayCommand(x =>
    {
        _quizes.ForEach(ServerHelper.SendNewQuiz);
        
        

    }, x => true);


    public ICommand LoadFromServer => new RelayCommand(x =>
    {
        _quizes.Clear();
        LoadQuizes();



    }, x => true);

    

}