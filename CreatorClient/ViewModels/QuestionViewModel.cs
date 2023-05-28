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

public class QuestionViewModel : NotifyPropertyChangedBase
{
    public QuestionViewModel(Question question)
    {
        Model = question;
    }

    public Question Model { get; set; }
    
    public int? Id { get => Model.Id; }
    public string Text
    {
        get => Model.Text; set
        {
            Model.Text= value;
            
            OnPropertyChanged(nameof(Text));
        }
    }

    public ObservableCollection<AnswerViewModel> Answers
    {
        get
        {
            var collection = new ObservableCollection<AnswerViewModel>();
            Model.Answers.ForEach(x => collection.Add(new AnswerViewModel(x)));
            return collection;
        }
    }

    public ICommand CreateAnswer => new RelayCommand(x =>
    {
        MessageBox.Show("Create");
        Model.Answers.Add(new Answer
        {
            Text = "Answer",
            IsCorrect= false
        });
        OnPropertyChanged(nameof(Answers));


    }, x => true);

}
