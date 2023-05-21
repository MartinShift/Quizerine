using CommonLibrary.LibraryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DbModels
{
    public class DbQuestion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public byte?[] Image { get; set; }
        public DbQuestion()
        {
            Answers = new HashSet<DbAnswer>();
        }
        public virtual ICollection<DbAnswer> Answers { get; set; }
    }
}
