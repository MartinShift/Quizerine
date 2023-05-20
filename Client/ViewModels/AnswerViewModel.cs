using CommonLibrary.LibraryModels;
using My.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels;

public class AnswerViewModel : NotifyPropertyChangedBase
{
    public AnswerViewModel(Answer answer) 
    {
        Model = answer;
    }
    public Answer Model { get; set; }

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
    public bool IsCorrect 
    {
        get => Model.IsCorrect;
        set
        {
            Model.IsCorrect = value;
            OnPropertyChanged(nameof(IsCorrect));
        }
    }
    public bool IsSelected 
    {
        get => Model.IsSelected;
        set
        {
            Model.IsSelected = value;
            OnPropertyChanged(nameof(IsSelected));
        }
    }
}
