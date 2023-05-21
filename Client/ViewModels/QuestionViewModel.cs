using CommonLibrary.LibraryModels;
using My.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels;

public class QuestionViewModel : NotifyPropertyChangedBase
{
    public QuestionViewModel(Question question)
    {
        Model = question;
    }
    public Question Model { get; set; }
    public int Id
    {
        get => Model.Id;
        set
        {
            Model.Id = value;
            OnPropertyChanged(nameof(Id));
        }
    }
    public string Text
    {
        get => Model.Text;
        set
        {
            Model.Text = value;
            OnPropertyChanged(nameof(Text));
        }
    }
    public ObservableCollection<AnswerViewModel> Answers 
    { 
        get
        {
            var collection = new ObservableCollection<AnswerViewModel>();
            Model.Answers.ForEach(a => collection.Add(new AnswerViewModel(a)));
            return collection;
        }
        set
        {
            Answers = value;
            OnPropertyChanged(nameof(Answers));
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
}
