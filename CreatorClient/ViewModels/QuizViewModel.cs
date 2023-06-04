using Client_Wpf.Models;
using CommonLibrary.LibraryModels;
using My.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CreatorClient.ViewModels;

public class QuizViewModel : NotifyPropertyChangedBase
{
    public QuizViewModel(Quiz quiz)
    {
        Model = quiz;
    }

    public Quiz Model { get; set; }

    public int? Id { get => Model.Id; }
    public string Title
    {
        get => Model.Title; set
        {
            Model.Title = value;

            OnPropertyChanged(nameof(Title));
        }
    }

    public int QuestionCount => Model.Questions.Count;

    public ObservableCollection<QuestionViewModel> Questions
    {
        get
        {
            var collection = new ObservableCollection<QuestionViewModel>();
            Model.Questions.ForEach(x => collection.Add(new QuestionViewModel(x)));
            return collection;
        }
    }

    public BitmapImage Image
    {
        get => Helper.ImageFromBytes(Model.Image); set
        {
            Model.Image = Helper.ImageToBytes(value);
            OnPropertyChanged(nameof(Image));
        }
    }

    public int TimeLimit
    {
        get => Model.TimeLimit; set
        {
            Model.TimeLimit = value;
            OnPropertyChanged(nameof(TimeLimit));
        }
    }

    private QuestionViewModel _selectedQuestion;
    public QuestionViewModel SelectedQuestion
    {
        get => _selectedQuestion; set
        {
            _selectedQuestion = value;
            OnPropertyChanged(nameof(SelectedQuestion));
           
                    }

    }

    public ICommand CreateQuestion => new RelayCommand(x =>
    {
        Model.Questions.Add(new Question
        {
            Text = "Question",
            Answers = new List<Answer> { new Answer { Text = "Answer 1"}, new Answer { Text = "Answer 2" } }
        });
        OnPropertyChanged(nameof(Questions));


    }, x => true);

    public ICommand LoadQuizLogo => new RelayCommand(x =>
    {
        Image = Helper.OpenFromFile();
        OnPropertyChanged(nameof(Image));
    });
}
