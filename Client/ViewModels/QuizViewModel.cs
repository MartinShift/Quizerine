using CommonLibrary.LibraryModels;
using My.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels;

public class QuizViewModel : NotifyPropertyChangedBase
{
    public QuizViewModel(Quiz quiz)
    {
        Model = quiz;
    }
    public Quiz Model { get; set; }
    public int? Id 
    {
        get => Model.Id;
        set
        {
            Model.Id = value;
            OnPropertyChanged(nameof(Id));
        }
    }
    public string Title 
    {
        get => Model.Title; 
        set
        {
            Model.Title = value;
            OnPropertyChanged(nameof(Title));
        }
    }
    public ObservableCollection<QuestionViewModel> Questions 
    { 
        get
        {
            var collection = new ObservableCollection<QuestionViewModel>();
            Model.Questions.ForEach(q => collection.Add(new QuestionViewModel(q)));
            return collection;
        }
        set
        {
            Questions = value;
            OnPropertyChanged(nameof(Questions));
        }
    }
    public byte[]? Image 
    {
        get => Model.Image;
        set
        {
            Model.Image = value;
            OnPropertyChanged(nameof(Image));
        }
    }
    public int TimeLimit 
    {
        get => Model.TimeLimit;
        set
        {
            Model.TimeLimit = value;
            OnPropertyChanged(nameof(TimeLimit));
        }
    }
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (!(obj is QuizViewModel))
            return false;

        return Model.Id.Equals((obj as QuizViewModel).Model.Id);
    }
}
