using CommonLibrary.LibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbModels
{
    public class DbQuiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DbQuiz()
        {
            Questions = new HashSet<Question>();
        }
        public byte?[] Image { get; set; }
        public int TimeLimit { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
