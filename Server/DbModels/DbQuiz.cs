using CommonLibrary.LibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbModels;

public class DbQuiz
{
    public DbQuiz()
    {
        Results = new HashSet<DbQuizResult>();
        Questions = new HashSet<DbQuestion>();
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public byte[]? Image { get; set; }
    public int TimeLimit { get; set; }
    public virtual ICollection<DbQuizResult> Results { get; set; }
    public virtual ICollection<DbQuestion> Questions { get; set; }
}
