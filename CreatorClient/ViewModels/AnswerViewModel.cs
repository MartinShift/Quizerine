using CommonLibrary.LibraryModels;
using My.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorClient.ViewModels;

public class AnswerViewModel : NotifyPropertyChangedBase
{
    public AnswerViewModel(Answer model)
    {
        Model = model;
    }

    public Answer Model { get; set; }
    
    public int? Id { get => Model.Id; }
    public string Text
    {
        get => Model.Text; set
        {
            Model.Text= value;
            
            OnPropertyChanged(nameof(Text));
        }
    }

    public bool IsCorrect
    {
        get => Model.IsCorrect; set
        {
            Model.IsCorrect = value;

            OnPropertyChanged(nameof(IsCorrect));
        }
    }



    //TODO
    //public ObservableCollection

}
