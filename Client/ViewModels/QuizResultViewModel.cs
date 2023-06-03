using CommonLibrary.LibraryModels;
using My.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModels;

public class QuizResultViewModel : NotifyPropertyChangedBase
{
    public QuizResultViewModel(QuizResult quizResult)
    {
        Model = quizResult;
        _positionInRecordTable = -1;
    }
    public QuizResultViewModel(QuizResult quizResult, int position)
    {
        Model = quizResult;
        _positionInRecordTable = position;
    }
    public QuizResult Model { get; set; }   

    public string ClientName 
    {
        get => Model.ClientName;
        set
        {
            Model.ClientName = value;
            OnPropertyChanged(nameof(ClientName));
        }
    }
    public QuizViewModel Quiz 
    { 
        get => new QuizViewModel(Model.Quiz);
        set
        {
            Quiz = value;
            OnPropertyChanged(nameof(Quiz));
        }
    }
    public int SecondsSpent 
    {
        get => Model.SecondsSpent;
        set
        {
            Model.SecondsSpent = value;
            OnPropertyChanged(nameof(SecondsSpent));
        }
    }
    public int Points 
    {
        get => Model.Points;
        set
        {
            Model.Points = value;
            OnPropertyChanged(nameof(Points));
        }
    }
    private int _positionInRecordTable;
    public int PositionInRecordTable
    {
        get => _positionInRecordTable;
        set
        {
            _positionInRecordTable = value;
            OnPropertyChanged(nameof(PositionInRecordTable));
        }
    }
}
